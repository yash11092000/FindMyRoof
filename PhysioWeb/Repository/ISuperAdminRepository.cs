using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface ISuperAdminRepository
    {
        Task<bool> SaveAgency(AgencyDetails model);
    }
}
