using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IPropertyRepository
    {
        Task<PropertyMaster> GetPropertyDetails(int propertyId);
    }
}
