using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sedixscope.web.Models.Domain;
using sedixscope.web.Models.ViewModels;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddLikeRequest request)
        {
            var blogLike = new BlogLike
            {
                BlogId = request.BlogPostId,
                DateAdded = DateTime.UtcNow,
                UserId = request.UserId
            };

            var like = await _blogPostLikeRepository.AddLikeForBlogAsync(blogLike);

            if (like != null)
            {
                return Ok();
            }
            
            return Problem("Problem occurred on our side", null, (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog(Guid blogPostId)
        {
            var totalLikes = await _blogPostLikeRepository.GetTotalLikesForBlogAsync(blogPostId);
            return Ok(totalLikes);
        }
    }
}