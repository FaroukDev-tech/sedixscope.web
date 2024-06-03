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

        public async Task<IdentityResult> AddUserAsync(IdentityUser identityUser, string password, bool isAdmin)
        {
            if (identityUser != null)
            {
                var identityResult = await _userManager.CreateAsync(identityUser, password);

                if (identityResult != null && identityResult.Succeeded)
                {
                    var roles = new List<string> { "User" };
                    
                    if (isAdmin)
                    {
                        roles.Add("Admin");
                    }

                    identityResult = await _userManager.AddToRolesAsync(identityUser, roles);
                    
                    return identityResult;
                }
            }

            return null;
        }

        public async Task<IdentityResult> DeleteAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                var identityResult = await _userManager.DeleteAsync(user);

                if (identityResult.Succeeded)
                {
                    return identityResult;
                }
            }

            return null;
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            var totalUsers = await _userManager.Users.ToListAsync();

            var superAdmin = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == "sadikalhanssah@gmail.com");

            if (superAdmin != null)
            {
                totalUsers.Remove(superAdmin);
            }

            return totalUsers;
        }
    }
}

