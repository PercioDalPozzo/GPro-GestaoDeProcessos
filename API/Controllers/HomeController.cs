using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
