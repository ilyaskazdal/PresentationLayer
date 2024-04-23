using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductPostsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<ProductPost> Products { get; set; } = new();

        public async Task<IActionResult> Index()
        {

            var products = _context.ProductPosts;
            return View(await products.ToListAsync());
        }

     
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductPostId,ProductPostName,ProductPostDesccription,CategoryId")] ProductPost product)
        {
            if (ModelState.IsValid)
            {
                product.ProductPostId = product.ProductPostId;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.Categories);
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (_context.ProductPosts == null)
            {
                return NotFound();
            }

            var product = await _context.ProductPosts.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.Categories);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductPostId,ProductPostName,ProductPostDesccription,CategoryId")] ProductPost product)
        {
            if (id != product.ProductPostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductPostId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.Categories);
            return View(product);
        }
        private bool ProductExists(int id)
        {
            return (_context.ProductPosts?.Any(e => e.ProductPostId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_context.ProductPosts == null)
            {
                return NotFound();
            }

            var product = await _context.ProductPosts
                .FirstOrDefaultAsync(m => m.ProductPostId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductPosts == null)
            {
                return Problem("Entity set 'ProductDbContext.ProductPosts'  is null.");
            }
            var product = await _context.ProductPosts.FindAsync(id);
            if (product != null)
            {
                _context.ProductPosts.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
