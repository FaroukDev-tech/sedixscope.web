using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sedixscope.web.Models;
using sedixscope.web.Models.ViewModels;
using sedixscope.web.Repository;

namespace sedixscope.web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogPostRepository _blogPostRepository;

    public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var blogPosts = await _blogPostRepository.GetAllAsync();

        var model = new HomeViewModel
        {
            BlogPosts = blogPosts
        }; 

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
