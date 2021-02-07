using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToiLamKythuat.Context;
using ToiLamKythuat.Models;
using ToiLamKythuat.ViewModels;

namespace ToiLamKythuat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _context;

        public HomeController(ILogger<HomeController> logger,
            BlogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeView()
            {
                categories = _context.Categories.ToList(),
                tags = _context.Tags.ToList(),
                topPosts = _context.Posts
                .Include("tags")
                .Include("categories")
                .Take(10).Select(x => new Post 
                { 
                    id = x.id,
                    title = x.title,
                    coverImage = x.coverImage,
                    summary = x.summary,
                    createDate = x.createDate,
                    metaTitle = x.metaTitle,
                    tags = x.tags,
                    categories = x.categories
                }),
                posts = _context.Posts
                .Include("tags")
                .Include(x => x.categories).ThenInclude(x => x.posts)
                .Select(x => new Post
                {
                    id = x.id,
                    title = x.title,
                    coverImage = x.coverImage,
                    summary = x.summary,
                    createDate = x.createDate,
                    metaTitle = x.metaTitle,
                    tags = x.tags,
                    categories = x.categories
                }),
            };
            return View(model);
        }

        public IActionResult Detail(int PostId)
        {
            var post = _context.Posts
                        .Include(x => x.categories).ThenInclude(x => x.posts)
                        .Include("tags")
                        .FirstOrDefault(x => x.id == PostId);

            var model = new DetailHomeView()
            {
                post = post,
                categories = _context.Categories.ToList(),
                tags = _context.Tags.ToList(),
                topPosts = _context.Posts.Take(10).Select(x => new Post
                {
                    id = x.id,
                    title = x.title,
                    coverImage = x.coverImage,
                    summary = x.summary,
                    createDate = x.createDate,
                    metaTitle = x.metaTitle
                })
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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
