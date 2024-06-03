using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sedixscope.web.Models.Domain;
using sedixscope.web.Models.ViewModels;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository blogPostCommentRepository)
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _blogPostCommentRepository = blogPostCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            var blogDetailView = new BlogDetailViewModel();
            var liked = false;

            if (blogPost != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikesForBlogAsync(id);

                if (_signInManager.IsSignedIn(User))
                {
                    var likesForBlog = await _blogPostLikeRepository.GetLikesForBlogAsync(id);

                    var userId = _userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                }

                var blogCommentsDomainModel = await _blogPostCommentRepository.GetCommentsForBlogAsync(id);

                var blogCommentsForView = new List<BlogCommentViewModel>();

                foreach (var blogComment in blogCommentsDomainModel)
                {
                    blogCommentsForView.Add(new BlogCommentViewModel
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = (await _userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
                    });
                }

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
                    UrlHandle = blogPost.UrlHandle,
                    Liked = liked,
                    Comments = blogCommentsForView
                };

                return View(blogDetailView);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(BlogDetailViewModel blogDetailViewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var commentDomainModel = new Comment
                {
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    BlogPostId = blogDetailViewModel.Id,
                    Description = blogDetailViewModel.CommentDescription,
                    DateAdded = DateTime.UtcNow
                };

                await _blogPostCommentRepository.AddAsync(commentDomainModel);
                return RedirectToAction("Detail", "Blogs", new { id = blogDetailViewModel.Id });
            }

            return Forbid();
        }
    }
}