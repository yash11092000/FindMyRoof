using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    public class PropertyDetailsController : Controller
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyDetailsController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
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
    }
}
