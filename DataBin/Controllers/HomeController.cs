using DataBin.Data;
using DataBin.Models;
using DataBin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DataBin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBinContext _context;

        public HomeController(ILogger<HomeController> logger, DataBinContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {
            var popular = _context.Post.OrderByDescending(post => post.Stars.Count()).Take(5);
            if (popular == null)
            {
                return NotFound();
            }

            var recent = _context.Post.OrderByDescending(r => r.CreatedAt).Take(5);

            if (recent == null)
            {
                return NotFound();
            }

            HomePagePosts viewModel = new HomePagePosts
            {
                MostStared = await popular.ToListAsync(),
                MostRecent = await recent.ToListAsync()
            };

            return View(viewModel);
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
}