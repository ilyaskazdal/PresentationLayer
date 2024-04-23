using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class Search : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
