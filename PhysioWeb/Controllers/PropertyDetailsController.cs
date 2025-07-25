using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;

namespace PhysioWeb.Controllers
{
    public class PropertyDetailsController : Controller
    {
        public IActionResult Property()
        {
            return View();
        }

    }
}
