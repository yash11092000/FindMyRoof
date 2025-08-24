using System.Data;
using System.Globalization;
using System.Reflection;
using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly DbHelper _dbHelper;

        public SuperAdminRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;

        }

        public async Task<bool> DeleteAgencyDetails(AgencyDetails AgencyDetails)
        {
            try
            {
                string[] parametersName = { "UniquId", "UserID" };
                object[] Values = { AgencyDetails.UniquId, AgencyDetails.UserID };

                string Sp = "FMR_DeleteAgencyDetails";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<AgencyDetails> EditAgencyDetails(int uniqueID, string? userID)
        {
            try
            {
                string[] parameterNames = { "UniqueID", "UserID" };
                object[] parameterValues = { uniqueID, userID };

                string Sp = "FMR_EditPropertyType";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
                while (data.Read())
                {
                    AgencyDetails AgencyDetails = new AgencyDetails(data, 1);
                    return AgencyDetails;
                }
                return null;

                //bind 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<DataTableResult> GetAllAgencies(DataTablePara dataTablePara)
        {
            try
            {
                string[] parameterName = new string[]
                {
                    "DisplayLength", "DisplayStart", "SortCol", "SortDir", "Search",
                    "AgencyName", "City", "IsActive", "CreatedBy"
                };

                object[] parameterValue = new object[]
                {
                    dataTablePara.iDisplayLength,
                    dataTablePara.iDisplayStart,
                    dataTablePara.iSortCol_0,
                    dataTablePara.sSortDir_0,
                    dataTablePara.sSearch,
                    dataTablePara.sSearch_0,  // AgencyName filter
                    dataTablePara.sSearch_1,  // City filter
                    dataTablePara.sSearch_2,  // IsActive filter
                    dataTablePara.sSearch_3   // CreatedBy filter
                };

                var reader = await _dbHelper.GetDataReaderAsync("[FMR_GetAllAgencies]", parameterName, parameterValue);

                var result = new DataTableResult();
                var list = new List<AgencyDetails>();

                while (reader.Read())
                {
                    list.Add(new AgencyDetails(reader));  // ✅ map from data reader
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        result.iTotalRecords = Convert.ToInt32(reader[0]);
                    }
                }

                result.iTotalDisplayRecords = result.iTotalRecords;
                result.aaData = list;

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<MenuMaster> GetMenuList(string role, string? userId)
        {
            try
            {
                string[] parametersName = { "Role", "UserId" };
                object[] Values = { role, userId };

                string Sp = "FMR_GetMenuList";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);
                MenuMaster result = new MenuMaster();
                while (data.Read())
                {
                    result.MenuList.Add(new MenuDetails(data));
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<Notification> GetNotifications()
        {
            try
            {
                string[] parametersName = { };
                object[] Values = { };

                string Sp = "FMR_GetNotifications";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);
                Notification result = new Notification();
                while (data.Read())
                {
                    result.NotiCount = Convert.ToInt32(data.GetValue(0));
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        result.Notifications.Add(Convert.ToString(data.GetValue(0)));
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> SaveAgency(AgencyDetails AgencyDetails)
        {
            try
            {
                string[] parametersName = {
                "UniquId", "AgencyName", "IsAgencyRegistered", "AgencyLogoFilePath", "AgencyLogoFileName",
                "ContactPersonName", "EmailAddress", "MobileNo", "AlternateMobileNo", "WebsiteUrl",
                "StreetAddress", "CityName", "StateName", "Pincode", "Country", "ReraRegNo",
                "LicenseIssueDate", "LicenseExpiryDate", "PAN", "GST", "IsActive",
                "ReraCertificateFilePath", "ReraCertificateFileName",
                "AgencyLicenseFilePath", "AgencyLicenseFileName",
                "AddressProofFilePath", "AddressProofFileName","UserName","Password","CreatedBy"
                };
                object[] Values = {
                AgencyDetails.UniquId,
                AgencyDetails.AgencyName,
                AgencyDetails.IsAgencyRegistered,
                AgencyDetails.AgencyLogoFilePath,
                AgencyDetails.AgencyLogoFileName,
                AgencyDetails.ContactPersonName,
                AgencyDetails.EmailAddress,
                AgencyDetails.MobileNo,
                AgencyDetails.AlternateMobileNo,
                AgencyDetails.WebsiteUrl,
                AgencyDetails.StreetAddress,
                AgencyDetails.CityName,
                AgencyDetails.StateName,
                AgencyDetails.Pincode,
                AgencyDetails.Country,
                AgencyDetails.ReraRegNo,
                string.IsNullOrEmpty(AgencyDetails.LicenseIssueDate)? (object)DBNull.Value: DateTime.ParseExact(AgencyDetails.LicenseIssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),         
                string.IsNullOrEmpty(AgencyDetails.LicenseExpiryDate)? (object)DBNull.Value: DateTime.ParseExact(AgencyDetails.LicenseExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                AgencyDetails.PAN,
                AgencyDetails.GST,
                AgencyDetails.IsActive,
                AgencyDetails.ReraCertificateFilePath,
                AgencyDetails.ReraCertificateFileName,
                AgencyDetails.AgencyLicenseFilePath,
                AgencyDetails.AgencyLicenseFileName,
                AgencyDetails.AddressProofFilePath,
                AgencyDetails.AddressProofFileName,
                AgencyDetails.UserName,
                AgencyDetails.Password,
                AgencyDetails.CreatedBy,
            };
                string Sp = "FMR_SaveAgencyDetails";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }

        public async Task<bool> SaveNotification(Notification notification)
        {
            try
            {
                string[] parametersName = { "Message", "Url", "ForRole", "IsRead" };
                object[] Values = { notification.Message, notification.Url, notification.ForRole, false };

                string Sp = "FMR_SaveNotification";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
