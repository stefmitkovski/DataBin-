using DataBin.Data;
using DataBin.Models;
using DataBin.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

            var comments = await _context.Comment.Where(c => c.PostId == id).ToListAsync();
            var star = await _context.Star.Where(s => s.PostId == id).FirstOrDefaultAsync();
            PostCommentSection viewModel = new PostCommentSection()
            {
                Post = post,
                Star = star,
                Comment = new Comment(),
                CommentSection = comments
            };

            return View(viewModel);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            PostCRUDViewModel viewModel = new PostCRUDViewModel()
            {
                Post = new Post(),
                Languages = new SelectList(_context.Language.AsEnumerable(), "Id", "Name"),
                TopicList = new MultiSelectList(_context.Topic.AsEnumerable(), "Id", "Name"),
                SelectedTopics = Enumerable.Empty<int>()
            };
            return View(viewModel);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCRUDViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Post.LanguageId = viewModel.Language;
                viewModel.Post.CreatedAt = DateTime.Now;
                if (string.IsNullOrWhiteSpace(viewModel.Post.Poster))
                {
                    viewModel.Post.Poster = "anonymous";
                }
                if (string.IsNullOrWhiteSpace(viewModel.Post.Title))
                {
                    viewModel.Post.Title = "Untitled";
                }
                _context.Add(viewModel.Post);
                await _context.SaveChangesAsync();
                if (viewModel.SelectedTopics != null)
                {
                    foreach (int topicId in viewModel.SelectedTopics)
                    {
                        _context.PostTopic.Add(new PostTopic { PostId = viewModel.Post.Id, TopicId = topicId });
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                foreach (var error in modelStateEntry.Errors)
                {
                    TempData["feedback"] = ($"Key: {key}, Error: {error.ErrorMessage}" + "\n");
                }
            }
            return RedirectToAction(nameof(Create));
        }

        // GET: Posts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post.Poster != User.Identity.Name)
            {
                return RedirectToAction(nameof(Index));
            }
            if (post == null)
            {
                return NotFound();
            }

            PostCRUDViewModel viewModel = new PostCRUDViewModel()
            {
                Post = post,
                Language = post.LanguageId,
                Languages = new SelectList(_context.Language.AsEnumerable(), "Id", "Name"),
                TopicList = new MultiSelectList(_context.Topic.AsEnumerable().OrderBy(s => s.Name), "Id", "Name"),
                SelectedTopics = _context.PostTopic.Where(s => s.PostId == id).Select(s => s.TopicId)
            };

            return View(viewModel);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, PostCRUDViewModel viewModel)
        {
            if (id != viewModel.Post.Id)
            {
                return NotFound();
            }
            if (viewModel.Post.Poster != User.Identity.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.Post.LastUpdatedAt = DateTime.Now;
                    viewModel.Post.LanguageId = viewModel.Language;
                    if (string.IsNullOrWhiteSpace(viewModel.Post.Title))
                    {
                        viewModel.Post.Title = "Untitled";
                    }
                    _context.Update(viewModel.Post);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> newTopicList = viewModel.SelectedTopics;
                    IEnumerable<int> prevTopicList = _context.PostTopic.Where(s => s.PostId == id).Select(s => s.TopicId);
                    IQueryable<PostTopic> toBeRemoved = _context.PostTopic.Where(s => s.PostId == id);
                    if (newTopicList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newTopicList.Contains(s.TopicId));
                        foreach (int topicId in newTopicList)
                        {
                            if (!prevTopicList.Any(s => s == topicId))
                            {
                                _context.PostTopic.Add(new PostTopic { TopicId = topicId, PostId = id });
                            }
                        }
                    }
                    _context.PostTopic.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(viewModel.Post.Id))
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
            return View(viewModel);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post.Poster != User.Identity.Name)
            {
                return RedirectToAction(nameof(Index));
            }
            if (post == null)
            {
                return NotFound();
            }


            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Post == null)
            {
                return Problem("Entity set 'DataBinContext.Post'  is null.");
            }
            var post = await _context.Post.FindAsync(id);
            if (post.Poster != User.Identity.Name)
            {
                return RedirectToAction(nameof(Index));
            }
            if (post != null)
            {
                _context.Post.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/MyPosts
        [Authorize]
        public async Task<IActionResult> MyPosts()
        {
            var posts = await _context.Post.Where(p => p.Poster == User.Identity.Name).ToListAsync();

            return View(posts);
        }

        // GET: Posts/Favorites
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var posts = await _context.Post
                .Where(p => p.Stars
                .Any(s => s.User == User.Identity.Name))
                .ToListAsync();
            if (posts == null)
            {
                return null;
            }
            else
            {
                return View(posts);
            }
        }

        private bool PostExists(int id)
        {
            return (_context.Post?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
