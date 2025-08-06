using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    [Authorize(Roles = "Agency")]
    public class AgentController : Controller
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly ISuperAdminRepository _superAdminRepository;
        public AgentController(IAgencyRepository agencyRepository, ISuperAdminRepository superAdminRepository)
        {
            _agencyRepository = agencyRepository;
            _superAdminRepository = superAdminRepository;
        }
        public IActionResult PropertyDesc()
        {
            return View();
        }

        #region Agency Dashboard
        public async Task<ActionResult> AgencyDashboard()
        {
            string username = User.Identity?.Name;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            string userId = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            MenuMaster menuMaster = await _superAdminRepository.GetMenuList(role, userId);
            return View(menuMaster);
        }
        #endregion
    }
}
