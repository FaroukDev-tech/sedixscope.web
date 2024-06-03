using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sedixscope.web.Models.ViewModels
{
    public class BlogCommentViewModel
    {
        [Required]
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        [Required]
        public string Username { get; set; }
    }
}