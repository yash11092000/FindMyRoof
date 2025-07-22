using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhysioWeb.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        public IActionResult PropertyDesc()
        {
            return View();
        }
    }
}
