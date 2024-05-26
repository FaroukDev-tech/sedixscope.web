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
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime DatePublished { get; set; }
        public string Author { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public int TotalLikes { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}