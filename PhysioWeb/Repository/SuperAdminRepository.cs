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
                    result.MenuList.Add(new DropDownSource(data));
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
                "AddressProofFilePath", "AddressProofFileName","UserName","Password"
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
                string.IsNullOrEmpty(AgencyDetails.LicenseIssueDate) ? (object)DBNull.Value : Convert.ToDateTime(AgencyDetails.LicenseIssueDate),
                string.IsNullOrEmpty(AgencyDetails.LicenseExpiryDate) ? (object)DBNull.Value : Convert.ToDateTime(AgencyDetails.LicenseExpiryDate),
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
    }
}
