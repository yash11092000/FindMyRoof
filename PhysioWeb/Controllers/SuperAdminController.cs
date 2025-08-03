using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly ISuperAdminRepository _superAdminRepository;
        public SuperAdminController(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }
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
            if (model == null)
                return Json(new { success = false, message = "Invalid request" });

            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "SecureUploads/" + model.AgencyName);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);


            //save Agency Logo
            if (model.AgencyLogo != null)
            {
                string logoDir = Path.Combine(basePath, "AgencyLogo");
                if (!Directory.Exists(logoDir)) Directory.CreateDirectory(logoDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(model.AgencyLogo.FileName);
                string filePath = Path.Combine(logoDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AgencyLogo.CopyToAsync(stream);
                }

                // Save filePath to DB (not the file itself)
                model.AgencyLogoFileName = fileName;
                model.AgencyLogoFilePath = filePath;
            }

            // ✅ Save RERA Certificate
            if (model.ReraCertificate != null)
            {
                string reraDir = Path.Combine(basePath, "ReraCertificates");
                if (!Directory.Exists(reraDir)) Directory.CreateDirectory(reraDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(model.ReraCertificate.FileName);
                string filePath = Path.Combine(reraDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ReraCertificate.CopyToAsync(stream);
                }

                model.ReraCertificateFileName = fileName;
                model.ReraCertificateFilePath = filePath;
            }

            // ✅ Save  Agency License
            if (model.AgencyLicense != null)
            {
                string reraDir = Path.Combine(basePath, "AgencyLicense");
                if (!Directory.Exists(reraDir)) Directory.CreateDirectory(reraDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(model.AgencyLicense.FileName);
                string filePath = Path.Combine(reraDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AgencyLicense.CopyToAsync(stream);
                }

                model.AgencyLicenseFileName = fileName;
                model.AgencyLicenseFilePath = filePath;
            }

            // ✅ Save Address Proof
            if (model.AddressProof != null)
            {
                string proofDir = Path.Combine(basePath, "AddressProofs");
                if (!Directory.Exists(proofDir)) Directory.CreateDirectory(proofDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(model.AddressProof.FileName);
                string filePath = Path.Combine(proofDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AddressProof.CopyToAsync(stream);
                }

                model.AddressProofFileName = fileName;
                model.AddressProofFilePath = filePath;
            }
            string hashed = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.Password = hashed;
            var result = await _superAdminRepository.SaveAgency(model);

            // ✅ Save logic here...
            return Json(new { success = true, message = "Agency saved successfully" });
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

        #endregion

        #region Dashboard
        public async Task<ActionResult> AdminDashboard()
        {
            string username = User.Identity?.Name;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            string userId = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            MenuMaster menuMaster = await _superAdminRepository.GetMenuList(role, userId);
            return View();
        }

        #endregion
    }
}
