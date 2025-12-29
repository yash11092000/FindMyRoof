using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    [Authorize]
    public class PropertyDetailsController : Controller
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IWhatsAppService _whatsAppService;
        private readonly ITelegramServices _telegramservice;

        public PropertyDetailsController(IPropertyRepository propertyRepository, IWhatsAppService whatsAppService, ITelegramServices telegramservice)
        {
            _propertyRepository = propertyRepository;
            _whatsAppService = whatsAppService;
            _telegramservice = telegramservice;
        }
        public async Task<ActionResult> Property(int PropertyId)
        {
            var PropertyDetails = await _propertyRepository.GetPropertyDetails(PropertyId);
            return View(PropertyDetails);
        }

        [Route("secure-videos/{*filePath}")]
        public IActionResult GetSecureVideo(string filePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "secure-images", filePath);
            fullPath = Path.GetFullPath(fullPath);

            if (!fullPath.StartsWith(Path.Combine(Directory.GetCurrentDirectory(), "secure-images")))
            {
                return BadRequest("Invalid video path");
            }

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            // Enable range requests for video streaming
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(fileStream, GetVideoContentType(fullPath), enableRangeProcessing: true);
        }

        private string GetVideoContentType(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return ext switch
            {
                ".mp4" => "video/mp4",
                ".webm" => "video/webm",
                ".ogg" => "video/ogg",
                _ => "application/octet-stream"
            };
        }
        //[Route("secure-images/{*filePath}")]
        //public IActionResult GetSecureImage(string filePath)
        //{
        //    var basePath = Path.Combine(Directory.GetCurrentDirectory());
        //    var fullPath = Path.Combine(basePath, filePath);

        //    if (!System.IO.File.Exists(fullPath))
        //        return NotFound();

        //    var contentType = "image/jpeg"; // Or detect based on extension
        //    return PhysicalFile(fullPath, contentType);
        //}

        #region request Demo
        public async Task<ActionResult> SendRequest(string ContactPersonName, string ContactPersonEmail, string ContactPersonPhone, string Description, int PropertyId)
        {
            string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            string AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _propertyRepository.SendRequest(ContactPersonName, ContactPersonEmail, ContactPersonPhone, Description, PropertyId);
            return Json(result);
        }
        #endregion

        #region whatsapp integration
        [HttpPost]
        public async Task<IActionResult> SendImage(List<string> MediaUrl)
        {
            if (MediaUrl == null || !MediaUrl.Any())
                return BadRequest("No media provided.");

            // Take first image (you can loop through all if needed)
            string relativePath = MediaUrl[0];

            // Convert to physical path on the server
            string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (!System.IO.File.Exists(physicalPath))
                return NotFound($"File not found: {physicalPath}");
            string uploadResponse = await _whatsAppService.UploadImageAsync(physicalPath);

            //string imageUrl = "https://example.com/myimage.jpg";
            //string caption = "Hello! Here's your image.";

            string result = await _whatsAppService.SendImageAsync("+918779791536", uploadResponse);
            return Content(result);
        }
        #endregion

        
    }
}
