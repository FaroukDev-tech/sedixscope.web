using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sedixscope.web.Models.ViewModels
{
    public class BlogCommentViewModel
    {
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string Username { get; set; }
    }
}