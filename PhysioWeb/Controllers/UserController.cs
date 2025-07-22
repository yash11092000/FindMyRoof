using Microsoft.AspNetCore.Mvc;

namespace PhysioWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
