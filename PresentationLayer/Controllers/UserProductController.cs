using BusinessLogicLayer.Abstract;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class UserProductController : Controller
    {
        private IProductPostRepo _ppRepo;

        public UserProductController(IProductPostRepo ppRepo)
        {
            _ppRepo = ppRepo;
        }

        public async Task<ActionResult> UserProductList()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");

            var products = _ppRepo.ProductPosts;

            products = products.Where(p => p.UserId == userId);

            return View(await products.ToListAsync());
        }



        [Authorize]
        public ActionResult CreateProduct()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult CreateProduct(UserProductCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                _ppRepo.CreateProductPost(
                    new ProductPost
                    {
                        ProductPostName = model.ProductPostName,
                        ProductPostDesccription = model.ProductPostDescription,
                        UserId = int.Parse(userId ?? ""),
                        DateTime = DateTime.Now,
                        StillOnSale = true

                    }
                );
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize]
        
        public ActionResult EditProduct(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var products = _ppRepo.ProductPosts.FirstOrDefault(i=>i.ProductPostId == id);

            if(products == null)
            { 
                return NotFound(); 
            }
            return View(new UserProductEditViewModel
            {
                ProductPostId = products.ProductPostId.ToString(),
                ProductPostName = products.ProductPostName,
                ProductPostDescription = products.ProductPostDesccription,
                StillOnSale = products.StillOnSale


            }) ;
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditProduct(UserProductEditViewModel model) 
        {
            if(ModelState.IsValid)
            {
                var update = new ProductPost
                {
                  ProductPostId = int.Parse(model.ProductPostId),
                    ProductPostName = model.ProductPostName,
                    ProductPostDesccription = model.ProductPostDescription,
                    StillOnSale = model.StillOnSale

                };
                _ppRepo.EditProductPost(update);
                return RedirectToAction("UserProductList");
            }
            return View(model);
        }

    }
}
