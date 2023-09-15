using DataBin.Data;
using DataBin.Models;
using DataBin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DataBin.Controllers
{
    public class PostsController : Controller
    {
        private readonly DataBinContext _context;

        public PostsController(DataBinContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string searchString, string PostTopic, string Language)
        {
            IQueryable<PostTopic> topicQuery = _context.PostTopic.AsQueryable();
            IQueryable<string> StringTopicQuery = _context.Topic.Distinct().Select(s => s.Name);
            IQueryable<string> StringLanguageQuery = _context.Language.Distinct().Select(s => s.Name);

            if (!string.IsNullOrEmpty(searchString))
            {
                topicQuery = topicQuery.Where(p => p.Post.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(Language))
            {
                topicQuery = topicQuery.Where(p => p.Post.Language.Name.Equals(Language));
            }

            if (!string.IsNullOrEmpty(PostTopic))
            {
                topicQuery = topicQuery.Where(p => p.Topic.Name == PostTopic);
            }

            // Load related data (Post and Topic)
            topicQuery = topicQuery.Include(p => p.Post).Include(p => p.Topic);

            // Create ViewModel
            ListingPosts viewModel = new ListingPosts
            {
                Posts = await topicQuery.Select(p => p.Post).Distinct().ToListAsync(),
                Topics = new SelectList(await StringTopicQuery.ToListAsync()),
                Languages = new SelectList(await StringLanguageQuery.ToListAsync())
            };

            return View(viewModel);
        }


        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Content,Stars,CreatedAt,LastUpdatedAt")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Content,Stars,CreatedAt,LastUpdatedAt")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Post == null)
            {
                return Problem("Entity set 'DataBinContext.Post'  is null.");
            }
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                _context.Post.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return (_context.Post?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
