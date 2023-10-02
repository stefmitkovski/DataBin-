using DataBin.Data;
using DataBin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataBin.Controllers
{
    public class StarsController : Controller
    {

        private readonly DataBinContext _context;

        public StarsController(DataBinContext context)
        {
            _context = context;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(PostCommentSection viewModel)
        {
            if (ModelState.IsValid)
            {
                // Query for the existing star with the same PostId and User
                var posts = await _context.Star
                    .Where(
                    s => s.User == viewModel.Star.User
                    &&
                    s.PostId == viewModel.Star.PostId
                    )
                    .FirstOrDefaultAsync();

                //if (posts == null)
                //{
                _context.Add(viewModel.Star);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = viewModel.Star.PostId });
                //}

                //return RedirectToAction("Details", "Posts", new { id = viewModel.Star.PostId });
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PostCommentSection viewModel)
        {
            if (ModelState.IsValid)
            {
                // Query for the existing star with the same PostId and User
                var posts = await _context.Star
                    .Where(
                    s => s.User == viewModel.Star.User
                    &&
                    s.PostId == viewModel.Star.PostId
                    )
                    .FirstOrDefaultAsync();


                // Ensure that the Id property is set
                int post_id = viewModel.Star.PostId;
                _context.Star.Remove(posts);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = post_id });

            }

            return NotFound();
        }
    }
}
