using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace sedixscope.web.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            var totalUsers =  await _userManager.Users.ToListAsync();

            var superAdmin =  await _userManager.Users.FirstOrDefaultAsync(x => x.Email=="sadikalhanssah@gmail.com");

            if (superAdmin != null)
            {
                totalUsers.Remove(superAdmin);
            }

            return totalUsers;
        }
    }
}