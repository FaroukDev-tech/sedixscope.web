using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sedixscope.web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
    }
}