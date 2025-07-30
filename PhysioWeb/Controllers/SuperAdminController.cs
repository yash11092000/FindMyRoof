using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;

namespace PhysioWeb.Controllers
{
    public class SuperAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Agency Details
        public async Task<ActionResult> AgencyDetails()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveAgency(AgencyDetails model)
        {
            // model.AgencyLogo, model.ReraCertificate, etc. will be filled automatically
            if (model == null || string.IsNullOrEmpty(model.AgencyName))
                return Json(new { success = false, message = "Invalid data" });

            // ✅ Save logic here...
            return Json(new { success = true, message = "Agency saved successfully" });
        }
        #endregion
    }
}
