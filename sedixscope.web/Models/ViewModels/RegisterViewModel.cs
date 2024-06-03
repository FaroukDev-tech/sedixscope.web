using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sedixscope.web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage="Username is too short. Must be at least 3 characters.")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage="Password must be at least 6 characters.")]
        public string Password { get; set; }
    }
}