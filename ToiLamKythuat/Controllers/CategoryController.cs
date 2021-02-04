using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToiLamKythuat.Context;
using ToiLamKythuat.Models;
using ToiLamKythuat.ViewModels;

namespace ToiLamKythuat.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BlogContext _context;

        public CategoryController(BlogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public async Task<IActionResult> Modal()
        {
            return PartialView(await _context.Categories.ToListAsync());
        }
        
        public IActionResult Post(string categoryCode)
        {
            var category = _context.Categories.FirstOrDefault(x => x.code == categoryCode);
            var model = new HomeView()
            {
                categories = _context.Categories.ToList(),
                tags = _context.Tags.ToList(),
                topPosts = _context.Posts
                .Include("tags")
                .Include("categories")
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
                posts = _context.Posts
                .Include("tags")
                .Include("categories")
                .Where(x => x.categories.Contains(category))
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

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.code == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("code,name,meta")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

       
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("code,name,meta")] Category category)
        {
            if (id != category.code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.code == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(e => e.code == id);
        }
    }
}
