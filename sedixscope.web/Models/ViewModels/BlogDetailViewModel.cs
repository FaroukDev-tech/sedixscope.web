using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sedixscope.web.Models.Domain;

namespace sedixscope.web.Models.ViewModels
{
    public class BlogDetailViewModel
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
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        public string CommentDescription { get; set; }
    }
}