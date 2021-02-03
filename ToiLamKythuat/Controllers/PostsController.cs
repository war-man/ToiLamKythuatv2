using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToiLamKythuat.Context;
using ToiLamKythuat.Models;

namespace ToiLamKythuat.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("id,title,createDate,summary,metaTitle,description,thumnailImage,coverImage,content,keywords,detail")] Post post, 
            List<Category> categories,
            List<Tag> tags)
        {
            if (ModelState.IsValid)
            {
                foreach(var category in categories)
                {
                    var link = _context.Categories.Find(category.code);
                    if(post.categories == null)
                    {
                        post.categories = new List<Category>();
                    }
                    post.categories.Add(link);
                }

                foreach (var tag in tags)
                {
                    var link = _context.Tags.Find(tag.id);
                    if (post.tags == null)
                    {
                        post.tags = new List<Tag>();
                    }
                    post.tags.Add(link);
                }

                _context.Posts.Add(post);
                
                await _context.SaveChangesAsync();
                if(post.id > 0)
                {
                    await _context.SaveChangesAsync();
                    return Json(new { state = "success", message = "Thêm mới thành công", model = post });
                }
                else
                {
                    return Json(new { state = "error", message = "Thêm mới thất bại"});
                }
            }
            return Json(new { state = "error", message = string.Join(";", ModelState.Values.Select(x => x.Errors))});
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Where(x => x.id == id)
                .Include("tags").Include("categories").AsNoTracking().FirstOrDefaultAsync();
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(long id, 
            [Bind("id,title,createDate,summary,metaTitle,description,thumnailImage,coverImage,content,keywords,detail")] Post post, 
            List<Category> categories,
            List<Tag> tags)
        {
            if (id != post.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(post).State = EntityState.Detached;
                    var updatePost = _context.Posts
                        .Include("categories")
                        .Include("tags")
                        .SingleOrDefault(x => x.id == id);

                    foreach (var category in updatePost.categories)
                    {
                        var checkLink = categories.FirstOrDefault(x => x.code == category.code);
                        if (checkLink == null)
                        {
                            var removeLink = _context.Categories.SingleOrDefault(x => x.code == category.code);
                            updatePost.categories.Remove(removeLink);
                        }
                    }

                    foreach (var tag in updatePost.tags)
                    {
                        var checkLink = tags.FirstOrDefault(x => x.id == tag.id);
                        if (checkLink == null)
                        {
                            var removeLink = _context.Tags.SingleOrDefault(x => x.id == tag.id);
                            updatePost.tags.Remove(removeLink);
                        }
                    }

                    updatePost.detail = post.detail;
                    updatePost.title = post.title;
                    updatePost.summary = post.summary;
                    updatePost.coverImage = post.coverImage;
                    updatePost.thumnailImage = post.thumnailImage;
                    updatePost.keywords = post.keywords;
                    updatePost.content = post.content;
                    updatePost.description = post.description;

                    foreach (var category in categories)
                    {
                        var oldLink = updatePost.categories?.FirstOrDefault(x => x.code == category.code);
                        if (oldLink == null)
                        {
                            var link = _context.Categories.SingleOrDefault(x => x.code == category.code);
                            if (updatePost.categories == null)
                            {
                                updatePost.categories = new List<Category>();
                            }
                            updatePost.categories.Add(link);
                        }
                    }

                    foreach (var tag in tags)
                    {
                        var oldLink = updatePost.tags?.FirstOrDefault(x => x.id == tag.id);
                        if (oldLink == null)
                        {
                            var link = _context.Tags.SingleOrDefault(x => x.id == tag.id);
                            if (updatePost.tags == null)
                            {
                                updatePost.tags = new List<Tag>();
                            }
                            updatePost.tags.Add(link);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(long id)
        {
            return _context.Posts.Any(e => e.id == id);
        }
    }
}
