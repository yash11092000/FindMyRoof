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
            // model.AgencyLogo, model.ReraCertificate, etc. will be filled automatically
            if (model == null)
                return Json(new { success = false, message = "Invalid request" });

            //string basePath = Path.Combine(Directory.GetCurrentDirectory(), "SecureUploads", model.AgencyName);
            //if (!Directory.Exists(basePath))
            //    Directory.CreateDirectory(basePath);


            ////save Agency Logo
            //if (model.AgencyLogo != null)
            //{
            //    string logoDir = Path.Combine(basePath, "AgencyLogo");
            //    if (!Directory.Exists(logoDir)) Directory.CreateDirectory(logoDir);

            //    string fileName = Guid.NewGuid() + Path.GetExtension(model.AgencyLogo.FileName);
            //    string filePath = Path.Combine(logoDir, fileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await model.AgencyLogo.CopyToAsync(stream);
            //    }

            //    // Save filePath to DB (not the file itself)
            //    model.AgencyLogoFileName = fileName;
            //    model.AgencyLogoFilePath = filePath;
            //}

            //// ✅ Save RERA Certificate
            //if (model.ReraCertificate != null)
            //{
            //    string reraDir = Path.Combine(basePath, "ReraCertificates");
            //    if (!Directory.Exists(reraDir)) Directory.CreateDirectory(reraDir);

            //    string fileName = Guid.NewGuid() + Path.GetExtension(model.ReraCertificate.FileName);
            //    string filePath = Path.Combine(reraDir, fileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await model.ReraCertificate.CopyToAsync(stream);
            //    }

            //    model.ReraCertificateFileName = fileName;
            //    model.ReraCertificateFilePath = filePath;
            //}

            //// ✅ Save  Agency License
            //if (model.AgencyLicense != null)
            //{
            //    string reraDir = Path.Combine(basePath, "AgencyLicense");
            //    if (!Directory.Exists(reraDir)) Directory.CreateDirectory(reraDir);

            //    string fileName = Guid.NewGuid() + Path.GetExtension(model.AgencyLicense.FileName);
            //    string filePath = Path.Combine(reraDir, fileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await model.AgencyLicense.CopyToAsync(stream);
            //    }

            //    model.AgencyLicenseFileName = fileName;
            //    model.AgencyLicenseFilePath = filePath;
            //}

            //// ✅ Save Address Proof
            //if (model.AddressProof != null)
            //{
            //    string proofDir = Path.Combine(basePath, "AddressProofs");
            //    if (!Directory.Exists(proofDir)) Directory.CreateDirectory(proofDir);

            //    string fileName = Guid.NewGuid() + Path.GetExtension(model.AddressProof.FileName);
            //    string filePath = Path.Combine(proofDir, fileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await model.AddressProof.CopyToAsync(stream);
            //    }

            //    model.AddressProofFileName = fileName;
            //    model.AddressProofFilePath = filePath;
            //}
            try
            {
                // Upload Agency Logo
                if (model.AgencyLogo != null)
                {
                    model.AgencyLogoFilePath = await _fileUploadService.UploadFile(
                        model.AgencyLogo,
                        model.AgencyName,
                        "AgencyLogo");
                }

                // Upload RERA Certificate
                if (model.ReraCertificate != null)
                {
                    model.ReraCertificateFilePath = await _fileUploadService.UploadFile(
                        model.ReraCertificate,
                        model.AgencyName,
                        "ReraCertificates");
                }

                // Upload Agency License
                if (model.AgencyLicense != null)
                {
                    model.AgencyLicenseFilePath = await _fileUploadService.UploadFile(
                        model.AgencyLicense,
                        model.AgencyName,
                        "AgencyLicense");
                }

                // Upload Address Proof
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

                // 2) Create persistent notification
                var notification = new Notification
                {
                    Message = $"New agency added: {model.AgencyName}",
                    Url = $"/SuperAdmin/AgencyDetails", // optional
                    ForRole = "SuperAdmin",
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };


                var response = await _superAdminRepository.SaveNotification(notification);

                // 2. Send notification to SuperAdmin group
                await _hubContext.Clients.Group("SuperAdmin")
                    .SendAsync("ReceiveNotification", $"New Agency Added: {model.AgencyName}");

                // ✅ Save logic here...
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
                sSortDir_0 = form["order[0][dir"],
                sSearch = form["search[value]"]
            };

            // ✅ Map column filters dynamically (for up to 30 columns)
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

            // ✅ Get data from Repository
            var result = await _superAdminRepository.GetAllAgencies(dataTablePara);

            // ✅ Return JSON response in DataTable format
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
