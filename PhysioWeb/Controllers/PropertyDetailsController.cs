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
        [Route("secure-images/{*filePath}")]
        public IActionResult GetSecureImage(string filePath)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            var fullPath = Path.Combine(basePath, filePath);

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var contentType = "image/jpeg"; // Or detect based on extension
            return PhysicalFile(fullPath, contentType);
        }
    }
}
