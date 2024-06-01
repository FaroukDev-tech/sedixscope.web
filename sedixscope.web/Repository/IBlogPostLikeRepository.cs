using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sedixscope.web.Models.Domain;

namespace sedixscope.web.Repository
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesForBlogAsync(Guid blogPostId);
        Task<BlogLike> AddLikeForBlogAsync(BlogLike blogPostLike);
        Task<IEnumerable<BlogLike>> GetLikesForBlogAsync(Guid blogPostId);
    }
}