using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sedixscope.web.Models.Domain;
using sedixscope.web.Models.ViewModels;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var newTag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            
            await _tagRepository.AddAsync(newTag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var existingTag = await _tagRepository.GetAsync(id);

            if (existingTag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = existingTag.Id,
                    Name = existingTag.Name,
                    DisplayName = existingTag.DisplayName
                };

                return View(editTagRequest);
            }
            
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var existingTag = await _tagRepository.GetAsync(editTagRequest.Id);

            if (existingTag != null)
            {
                var tag = new Tag
                {
                    Id = editTagRequest.Id,
                    Name = editTagRequest.Name,
                    DisplayName = editTagRequest.DisplayName
                };

                await _tagRepository.UpdateAsync(tag);

                return RedirectToAction("List");
            }

            return View();
        }
    }
}