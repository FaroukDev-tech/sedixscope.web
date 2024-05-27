using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sedixscope.web.Data;
using sedixscope.web.Models.Domain;

namespace sedixscope.web.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly SedixDbContext _sedixDbContext;
        public TagRepository(SedixDbContext sedixDbContext)
        {
            _sedixDbContext = sedixDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _sedixDbContext.Tags.AddAsync(tag);
            await _sedixDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> DeleteAsync(Guid id)
        {
            var existingTag = await _sedixDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                _sedixDbContext.Tags.Remove(existingTag);
                await _sedixDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _sedixDbContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            var existingTag = await _sedixDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            return existingTag;
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            var existingTag = await _sedixDbContext.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _sedixDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }
    }
}