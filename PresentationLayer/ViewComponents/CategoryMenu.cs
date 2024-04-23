using BusinessLogicLayer.Abstract;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents
{
    public class CategoryMenu:ViewComponent
    {
        private ICategoryRepo CategoryRepo;

        public CategoryMenu(ICategoryRepo categoryRepo)
        {
            CategoryRepo = categoryRepo;
        }

        public IViewComponentResult Invoke()
        {
            return View(CategoryRepo.Categories.ToList());
        }
    }
}
