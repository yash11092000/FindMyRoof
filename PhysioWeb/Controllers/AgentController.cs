using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhysioWeb.Controllers
{
    [Authorize(Roles = "Agency")]
    public class AgentController : Controller
    {
        public IActionResult PropertyDesc()
        {
            return View();
        }

        #region Agency Dashboard
        public async Task<ActionResult> AgencyDashboard() {
            return View();
        }
        #endregion
    }
}
