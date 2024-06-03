using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using sedixscope.web.Models.ViewModels;

namespace sedixscope.web.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IdentityResult> DeleteAsync(Guid userId);
        Task<IdentityResult> AddUserAsync(IdentityUser identityUser, string password, bool isAdmin);
    }
}