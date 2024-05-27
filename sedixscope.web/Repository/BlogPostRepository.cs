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
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly SedixDbContext _sedixDbContext;
        public BlogPostRepository(SedixDbContext sedixDbContext)
        {
            _sedixDbContext = sedixDbContext;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _sedixDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(Guid id)
        {
            return await _sedixDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _sedixDbContext.BlogPosts.AddAsync(blogPost);
            await _sedixDbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var existingBlog = await _sedixDbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                _sedixDbContext.BlogPosts.Remove(existingBlog);
                await _sedixDbContext.SaveChangesAsync();

                return existingBlog;
            }

            return null;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await _sedixDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                {
                    existingBlog.Heading = blogPost.Heading;
                    existingBlog.Content = blogPost.Content;
                    existingBlog.ShortDescription = blogPost.ShortDescription;
                    existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                    existingBlog.UrlHandle = blogPost.UrlHandle;
                    existingBlog.DatePublished = blogPost.DatePublished;
                    existingBlog.Author = blogPost.Author;
                    existingBlog.Visible = blogPost.Visible;
                    existingBlog.Tags = blogPost.Tags;

                    await _sedixDbContext.SaveChangesAsync();
                    return existingBlog;
                };
            }

            return null;
        }
    }
}