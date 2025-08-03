using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface ISuperAdminRepository
    {
        Task<DataTableResult> GetAllAgencies(DataTablePara dataTablePara);
        Task<MenuMaster> GetMenuList(string? role, string? userId);
        Task<bool> SaveAgency(AgencyDetails model);
    }
}
