using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackOverflow.Models;

namespace StackOverflow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public PageViewModel ViewModel { get; set; }


        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ViewResult> Home(int page = 1)
        {
            var pagesize = 3;
            
            var appContext = _context.Questions.Include(q => q.Creator);
            var count = appContext.Count();
            var items = appContext
                .OrderByDescending(x => x.LastActivity)
                .Skip((page - 1) * pagesize)
                .Take(pagesize); 

            var viewModel = new IndexViewModel()
            {
                VierwModel = new PageViewModel(count, page,pagesize),
                Questions = items
            };

            return View(viewModel);
        }

        public async Task<ViewResult> Search(string searchText)
        {
            var appContext = _context.Questions.Include(q => q.Creator);
            var items = await appContext
                .Where(x => x.Topic.Contains(searchText))
                .OrderByDescending(x => x.LastActivity)
                .ToListAsync();
            return View("Home", new IndexViewModel()
            {
                VierwModel = new PageViewModel(items.Count, 1, items.Count),
                Questions = items
            });
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
