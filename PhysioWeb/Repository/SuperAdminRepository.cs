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
    "AddressProofFilePath", "AddressProofFileName"
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
    AgencyDetails.AddressProofFileName
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
