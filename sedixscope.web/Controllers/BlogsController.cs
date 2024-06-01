using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sedixscope.web.Models.ViewModels;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            var blogDetailView = new BlogDetailViewModel();

            if (blogPost != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikesForBlogAsync(id);

                blogDetailView = new BlogDetailViewModel
                {
                    Id = blogPost.Id,
                    PageTitle = blogPost.PageTitle,
                    Heading = blogPost.Heading,
                    Content = blogPost.Content,
                    DatePublished = blogPost.DatePublished,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    TotalLikes = totalLikes,
                    ShortDescription = blogPost.ShortDescription,
                    Tags = blogPost.Tags,
                    UrlHandle = blogPost.UrlHandle
                };

                return View(blogDetailView);
            }

            return View(null);
        }
    }
}