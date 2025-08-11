using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Win32;
using PhysioWeb.Hubs;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly ISuperAdminRepository _superAdminRepository;
        private readonly FileUploadService _fileUploadService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public SuperAdminController(ISuperAdminRepository superAdminRepository, IHubContext<NotificationHub> hubContext, FileUploadService fileUploadService)
        {
            _superAdminRepository = superAdminRepository;
            _hubContext = hubContext;
            _fileUploadService = fileUploadService;

        }

        #region Agency Details
        public async Task<ActionResult> AgencyDetails()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveAgency(AgencyDetails model)
        {
            if (model == null)
                return Json(new { success = false, message = "Invalid request" });
            try
            {
                if (model.AgencyLogo != null)
                {
                    model.AgencyLogoFilePath = await _fileUploadService.UploadFile(
                        model.AgencyLogo,
                        model.AgencyName,
                        "AgencyLogo");
                }
                if (model.ReraCertificate != null)
                {
                    model.ReraCertificateFilePath = await _fileUploadService.UploadFile(
                        model.ReraCertificate,
                        model.AgencyName,
                        "ReraCertificates");
                }
                if (model.AgencyLicense != null)
                {
                    model.AgencyLicenseFilePath = await _fileUploadService.UploadFile(
                        model.AgencyLicense,
                        model.AgencyName,
                        "AgencyLicense");
                }
                if (model.AddressProof != null)
                {
                    model.AddressProofFilePath = await _fileUploadService.UploadFile(
                        model.AddressProof,
                        model.AgencyName,
                        "AddressProofs");
                }
                string hashed = BCrypt.Net.BCrypt.HashPassword(model.Password);
                model.Password = hashed;
                model.CreatedBy = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
                var result = await _superAdminRepository.SaveAgency(model);

                var notification = new Notification
                {
                    Message = $"New agency added: {model.AgencyName}",
                    Url = $"/SuperAdmin/AgencyDetails",
                    ForRole = "SuperAdmin",
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };
                var response = await _superAdminRepository.SaveNotification(notification);
                await _hubContext.Clients.Group("SuperAdmin")
                    .SendAsync("ReceiveNotification", $"New Agency Added: {model.AgencyName}");

                return Json(new { success = true, message = "Agency saved successfully" });
            }
            catch (Exception e)
            {

            }
            return Json(new { success = true, message = "Agency Save Unsuccessfull" });
        }

        [HttpPost]
        public async Task<ActionResult> GetAllAgencies()
        {
            var form = Request.Form;

            // ✅ Map DataTables parameters
            var dataTablePara = new DataTablePara
            {
                iDisplayStart = Convert.ToInt32(form["start"]),
                iDisplayLength = Convert.ToInt32(form["length"]),
                iSortCol_0 = Convert.ToInt32(form["order[0][column]"]),
                sSortDir_0 = form["order[0][dir]"],
                sSearch = form["search[value]"]
            };
            for (int i = 0; i < 30; i++)
            {
                string key = $"columns[{i}][search][value]";
                if (Request.Form.ContainsKey(key))
                {
                    typeof(DataTablePara)
                        .GetProperty($"sSearch_{i}")
                        ?.SetValue(dataTablePara, Request.Form[key].ToString());
                }
            }
            var result = await _superAdminRepository.GetAllAgencies(dataTablePara);
            return Json(new
            {
                draw = form["draw"],
                recordsTotal = result.iTotalRecords,
                recordsFiltered = result.iTotalDisplayRecords,
                data = result.aaData
            });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAgencyDetails(int UniqueID)
        {
            var AgencyDetails = new AgencyDetails
            {
                UniquId = UniqueID
            };
            AgencyDetails.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var result = await _superAdminRepository.DeleteAgencyDetails(AgencyDetails);
            return Json(new { success = result });
        }


        [HttpPost]
        public async Task<ActionResult> EditAgencyDetails(int UniqueID)
        {
            try
            {
                string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
                var data = await _superAdminRepository.EditAgencyDetails(UniqueID, UserID);
                return Json(data);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Dashboard
        public async Task<ActionResult> AdminDashboard()
        {
            string username = User.Identity?.Name;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            string userId = User.FindFirst(ClaimTypes.PrimarySid)?.Value;

            MenuMaster menuMaster = await _superAdminRepository.GetMenuList(role, userId);

            return View(menuMaster);
        }
        public async Task<ActionResult> GetNotifications()
        {
            Notification Notification = await _superAdminRepository.GetNotifications();
            return Json(Notification);
        }
        #endregion
    }
}
