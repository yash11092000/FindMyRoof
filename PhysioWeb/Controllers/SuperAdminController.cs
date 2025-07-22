using Microsoft.AspNetCore.Mvc;

namespace PhysioWeb.Controllers
{
    public class SuperAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
