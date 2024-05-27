using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sedixscope.web.Models.Domain;
using sedixscope.web.Models.ViewModels;

namespace sedixscope.web.Repository
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetByIdAsync(Guid id);
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost> UpdateAsync(BlogPost blogPost);
        Task<BlogPost> DeleteAsync(Guid id);
    }
}