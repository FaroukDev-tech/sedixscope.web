using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sedixscope.web.Models.ViewModels
{
    public class AddBlogPostComment
    {
        public Guid UserId { get; set; }
        public Guid BlogPostId { get; set; }
    }
}