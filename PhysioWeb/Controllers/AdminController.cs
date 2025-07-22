using Microsoft.AspNetCore.Mvc;

namespace PhysioWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
