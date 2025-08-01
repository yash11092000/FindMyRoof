using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
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

            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "SecureUploads");
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            if (model.AgencyLogo != null)
            {
                string logoDir = Path.Combine(basePath, "AgencyLogo");
                if (!Directory.Exists(logoDir)) Directory.CreateDirectory(logoDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(model.AgencyLogo.FileName);
                string filePath = Path.Combine(logoDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AgencyLicense.CopyToAsync(stream);
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
                model.AgencyLogoFilePath = filePath;
            }

            var result = await _superAdminRepository.SaveAgency(model);

            // ✅ Save logic here...
            return Json(new { success = true, message = "Agency saved successfully" });
        }
        #endregion
    }
}
