using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sedixscope.web.Data;
using sedixscope.web.Models.Domain;
using sedixscope.web.Models.ViewModels;

namespace sedixscope.web.Repository
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly SedixDbContext _sedixDbContext;
        public BlogPostCommentRepository(SedixDbContext sedixDbContext)
        {
            _sedixDbContext = sedixDbContext;
        }
        public async Task<Comment> AddAsync(AddBlogPostComment addBlogPostComment)
        {
            var blogComment = new Comment
            {
                UserId = addBlogPostComment.UserId,
                BlogPostId = addBlogPostComment.BlogPostId,
                DateAdded = DateTime.UtcNow
            };

            await _sedixDbContext.Comments.AddAsync(blogComment);
            await _sedixDbContext.SaveChangesAsync();

            return blogComment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsForBlogAsync(Guid blogPostId)
        {
            return await _sedixDbContext.Comments.Where(x => x.BlogPostId==blogPostId).ToListAsync();
        }
    }
}