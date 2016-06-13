using Microsoft.AspNetCore.Mvc;

namespace RockPaperScissors.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
