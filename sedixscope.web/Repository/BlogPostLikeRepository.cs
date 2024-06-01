using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sedixscope.web.Data;
using sedixscope.web.Models.Domain;

namespace sedixscope.web.Repository
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly SedixDbContext _sedixDbContext;
        public BlogPostLikeRepository(SedixDbContext sedixDbContext)
        {
            _sedixDbContext = sedixDbContext;
        }
        public async Task<BlogLike> AddLikeForBlogAsync(BlogLike blogPostLike)
        {
            await _sedixDbContext.BlogLikes.AddAsync(blogPostLike);
            await _sedixDbContext.SaveChangesAsync();

            return blogPostLike;
        }

        public async Task<IEnumerable<BlogLike>> GetLikesForBlogAsync(Guid blogPostId)
        {
            return await _sedixDbContext.BlogLikes.Where(x => x.BlogId==blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikesForBlogAsync(Guid blogPostId)
        {
            return await _sedixDbContext.BlogLikes.CountAsync(x => x.BlogId==blogPostId);
        }
    }
}