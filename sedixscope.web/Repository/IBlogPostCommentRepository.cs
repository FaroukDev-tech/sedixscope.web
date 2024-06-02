using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sedixscope.web.Models.Domain;
using sedixscope.web.Models.ViewModels;

namespace sedixscope.web.Repository
{
    public interface IBlogPostCommentRepository
    {
        Task<Comment> AddAsync(AddBlogPostComment addBlogPostComment);
        Task<IEnumerable<Comment>> GetCommentsForBlogAsync(Guid blogPostId);
    }
}