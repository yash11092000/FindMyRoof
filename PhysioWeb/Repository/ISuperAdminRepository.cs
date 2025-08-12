using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface ISuperAdminRepository
    {
        Task<bool> DeleteAgencyDetails(AgencyDetails agencyDetails);
        Task<AgencyDetails> EditAgencyDetails(int uniqueID, string? userID);
        Task<DataTableResult> GetAllAgencies(DataTablePara dataTablePara);
        Task<MenuMaster> GetMenuList(string? role, string? userId);
        Task<Notification> GetNotifications();
        Task<bool> SaveAgency(AgencyDetails model);
        Task<bool> SaveNotification(Notification notification);
    }
}
