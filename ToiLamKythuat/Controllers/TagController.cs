using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToiLamKythuat.Context;
using ToiLamKythuat.Models;
using ToiLamKythuat.ViewModels;

namespace ToiLamKythuat.Controllers
{
    public class TagController : Controller
    {
        private readonly BlogContext _context;

        public TagController(BlogContext context)
        {
            _context = context;
        }

        // GET: Tag
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tags.ToListAsync());
        }

        public async Task<IActionResult> Modal()
        {
            return PartialView(await _context.Tags.ToListAsync());
        }

        public IActionResult Post(int tagId)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.id == tagId);
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
                .Where(x => x.tags.Contains(tag))
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

        // GET: Tag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,meta,tagName")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,meta,tagName")] Tag tag)
        {
            if (id != tag.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.id))
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
            return View(tag);
        }

        // GET: Tag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.id == id);
        }
    }
}
