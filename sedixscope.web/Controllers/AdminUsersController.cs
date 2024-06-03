using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sedixscope.web.Models.ViewModels;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AdminUsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userRequestModel = new UserRequest();
            userRequestModel.Users = new List<UserViewModel>();

            if (users != null)
            {
                foreach (var user in users)
                {
                    userRequestModel.Users.Add(new UserViewModel
                    {
                        Id = Guid.Parse(user.Id),
                        Username = user.UserName,
                        Email = user.Email
                    });
                }

                return View(userRequestModel);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserRequest userRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = userRequest.Username,
                Email = userRequest.Email
            };

            bool isAdmin = userRequest.AdminRoleCheckBox;
            var identityResult = await _userRepository.AddUserAsync(identityUser, userRequest.Password, isAdmin);

            if (identityResult != null && identityResult.Succeeded)
            {
                return RedirectToAction("List", "AdminUsers");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userRepository.DeleteAsync(id);

            if (result != null && result.Succeeded)
            {
                return RedirectToAction("List", "AdminUsers");
            }

            return View();
        }
    }
}