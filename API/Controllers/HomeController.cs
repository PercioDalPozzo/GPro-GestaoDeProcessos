using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Home")]
    public class HomeController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
