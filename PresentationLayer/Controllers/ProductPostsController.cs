using BusinessLogicLayer.Abstract;
using BusinessLogicLayer.Concrete.EfCore;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class ProductPostsController : Controller
    {
        private IProductPostRepo _ppRepo;
        private ICommentRepo _commentRepo;
        private ICategoryRepo _categoryRepo;

        public ProductPostsController(IProductPostRepo ppRepo, ICommentRepo commentRepo, ICategoryRepo categoryRepo)
        {
            _ppRepo = ppRepo;
            _commentRepo = commentRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View(new PPViewModel
            {
                ProductPosts = _ppRepo.ProductPosts.ToList()

            }
            );
        }
        public async Task<IActionResult> Details(int? id) {

        return View(await _ppRepo
            .ProductPosts
            .Include(x=>x.User)
            .Include(x=>x.Comments)
            .ThenInclude(x =>x.User)
            .FirstOrDefaultAsync(p=>p.ProductPostId == id));
            
        }

        public async Task<IActionResult> Categories(int id)
        {
            var posts = _ppRepo.ProductPosts;
            posts = posts.Where(x => x.Categories.Any(t => t.CategoryId == id));
            return View(
                new PPViewModel
                {
                    ProductPosts = await posts.ToListAsync()
                });

        }
        public IActionResult AddComment(int PPId, string CommentText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var com = new Comment
            {
                CommentText = CommentText,
                CommentPublishDate = DateTime.Now,
                ProductPostId = PPId,
                UserId = int.Parse(userId ??"")

            };
            _commentRepo.CreateComment(com);
          return Redirect("/Product/Details/" + PPId);
            
        }

        [HttpGet]
        [Route("SearchProduct")]
        public IActionResult SearchProduct(String searchString)
        {


            var products = from u in _ppRepo.ProductPosts
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(u => u.ProductPostName.Contains(searchString));

                var foundProduct = products.FirstOrDefault();
                if (foundProduct != null)
                {
                    return RedirectToAction("Details", "ProductPosts", new { id = foundProduct.ProductPostId });

                }
            }

            return View(products.ToList());


        }

        [HttpGet]
        [Route("SearchCategory")]
        public IActionResult SearchCategory(String searchString)
        {


            var categories = from u in _categoryRepo.Categories
                           select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(u => u.CategoryName.Contains(searchString));

                var foundCategory = categories.FirstOrDefault();
                if (foundCategory != null)
                {
                    return RedirectToAction("Categories", "ProductPosts", new { id = foundCategory.CategoryId });

                }
            }

            return View(categories.ToList());


        }

        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _ppRepo.DeleteProduct(id);
            return RedirectToAction("Index", "ProductPosts");
        }

    }
}
