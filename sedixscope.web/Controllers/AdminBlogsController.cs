using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using sedixscope.web.Models.Domain;
using sedixscope.web.Models.ViewModels;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;
        public AdminBlogsController(IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name})
            };

            return View(model);
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
                DatePublished = DateTime.UtcNow,
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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var existingBlogPost = await _blogPostRepository.GetByIdAsync(id);
            var allTags = await _tagRepository.GetAllAsync();

            if (existingBlogPost != null)
            {
                var editBlogPost = new EditBlogPostRequest
                {
                    Id = existingBlogPost.Id,
                    Heading = existingBlogPost.Heading,
                    Content = existingBlogPost.Content,
                    ShortDescription = existingBlogPost.ShortDescription,
                    FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
                    UrlHandle = existingBlogPost.UrlHandle,
                    Author = existingBlogPost.Author,
                    Visible = existingBlogPost.Visible,
                    Tags = allTags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = existingBlogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(editBlogPost);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var existingBlogPost = await _blogPostRepository.GetByIdAsync(editBlogPostRequest.Id);

            if (existingBlogPost != null)
            {
                var blogPost = new BlogPost
                {
                    Id = editBlogPostRequest.Id,
                    Heading = editBlogPostRequest.Heading,
                    Content = editBlogPostRequest.Content,
                    ShortDescription = editBlogPostRequest.ShortDescription,
                    FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                    UrlHandle = editBlogPostRequest.UrlHandle,
                    Author = editBlogPostRequest.Author,
                    Visible = editBlogPostRequest.Visible,
                };

                var selectedTags = new List<Tag>();

                foreach (var tag in editBlogPostRequest.SelectedTags)
                {
                    var selectedTagIdAsGuid = Guid.Parse(tag);
                    var selectedTag = await _tagRepository.GetAsync(selectedTagIdAsGuid);

                    if (selectedTag != null)
                    {
                        selectedTags.Add(selectedTag);
                    }
                }

                blogPost.Tags = selectedTags;

                await _blogPostRepository.UpdateAsync(blogPost);

                return RedirectToAction("List");
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedPost = await _blogPostRepository.DeleteAsync(id);

            if (deletedPost==null)
            {
                return View(null);
            }

            return RedirectToAction("List");
        }
    }
}