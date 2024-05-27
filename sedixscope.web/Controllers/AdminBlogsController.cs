using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sedixscope.web.Models.Domain;
using sedixscope.web.Models.ViewModels;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers
{
    public class AdminBlogsController : Controller
    {
        private readonly BlogPostRepository _blogPostRepository;
        private readonly TagRepository _tagRepository;
        public AdminBlogsController(BlogPostRepository blogPostRepository, TagRepository tagRepository)
        {
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Id = addBlogPostRequest.Id,
                Heading = addBlogPostRequest.Heading,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                DatePublished = addBlogPostRequest.DatePublished,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible
            };

            var selectedTags = new List<Tag>();

            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            blogPost.Tags = selectedTags;

            await _blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }
    }
}