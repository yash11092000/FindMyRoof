using Microsoft.AspNetCore.Mvc;

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
