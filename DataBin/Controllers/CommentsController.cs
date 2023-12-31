﻿using DataBin.Data;
using DataBin.Models;
using DataBin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataBin.Controllers
{
    public class CommentsController : Controller
    {
        private readonly DataBinContext _context;

        public CommentsController(DataBinContext context)
        {
            _context = context;
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(PostCommentSection viewModel)
        {
            viewModel.Comment.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = viewModel.Comment.PostId });
            }
            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                foreach (var error in modelStateEntry.Errors)
                {
                    TempData["feedback"] = ($"Key: {key}, Error: {error.ErrorMessage}" + "\n");
                }
            }
            return RedirectToAction("Details", "Posts", new { id = viewModel.Comment.PostId });
        }

        // GET: Comments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var verify = _context.Comment.Where(c => c.Id == id && c.Poster == User.Identity.Name).FirstOrDefaultAsync();
            if (verify == null)
            {
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Poster,CreatedAt,LastUpdatedAt,PostId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            comment.LastUpdatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    var verify = await _context.Comment
                        .AsNoTracking()
                        .Where(c => c.Id == id && c.Poster == User.Identity.Name)
                        .FirstOrDefaultAsync();

                    if (verify == null)
                    {
                        return RedirectToAction("Details", "Posts", new { id = comment.PostId });
                    }

                    _context.Update(comment);
                    await _context.SaveChangesAsync(); // Ensure proper await here
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }

            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                foreach (var error in modelStateEntry.Errors)
                {
                    TempData["feedback"] = ($"Key: {key}, Error: {error.ErrorMessage}" + "\n");
                }
            }
            return RedirectToAction("Details", "Posts", new { id = comment.PostId });
        }

        // GET: Comments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            var verify = _context.Comment.Where(c => c.Id == id && c.Poster == User.Identity.Name).FirstOrDefaultAsync();
            if (verify == null)
            {
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'DataBinContext.Comment'  is null.");
            }

            var verify = await _context.Comment
                .Where(c => c.Id == id && c.Poster == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (verify == null)
            {
                return RedirectToAction("Details", "Posts", new { id = id });
            }

            var comment = await _context.Comment.FindAsync(id);

            if (comment != null)
            {
                _context.Comment.Remove(comment);
                await _context.SaveChangesAsync(); // Ensure proper await here
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }

            return RedirectToAction("Details", "Posts", new { id = id });
        }


        private bool CommentExists(int id)
        {
            return (_context.Comment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
