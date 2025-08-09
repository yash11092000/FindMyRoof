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

    }
}
