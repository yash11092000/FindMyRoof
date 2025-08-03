using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace PhysioWeb.Models
{
    public class AgencyDetails : CommanProp
    {
        public string AgencyName { get; set; }
        public bool IsAgencyRegistered { get; set; } = false;

        // ✅ Logo
        public IFormFile AgencyLogo { get; set; }
        public string AgencyLogoFilePath { get; set; }
        public string AgencyLogoFileName { get; set; }

        // ✅ Contact Info
        public string ContactPersonName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string WebsiteUrl { get; set; }

        // ✅ Address
        public string StreetAddress { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }

        // ✅ Registration Info
        public string ReraRegNo { get; set; }
        public string LicenseIssueDate { get; set; }
        public string LicenseExpiryDate { get; set; }
        public string PAN { get; set; }
        public string GST { get; set; }

        public bool IsActive { get; set; } = true;

        public string IsActiveText { get; set; }

        // ✅ Documents
        public IFormFile ReraCertificate { get; set; }
        public string ReraCertificateFilePath { get; set; }
        public string ReraCertificateFileName { get; set; }

        public IFormFile AgencyLicense { get; set; }
        public string AgencyLicenseFilePath { get; set; }
        public string AgencyLicenseFileName { get; set; }

        public IFormFile AddressProof { get; set; }
        public string AddressProofFilePath { get; set; }
        public string AddressProofFileName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        // ✅ For Handling Uploads in Forms
        //public IFormFile AgencyLogoFile { get; set; }
        //public IFormFile ReraCertificateFile { get; set; }
        //public IFormFile AgencyLicenseFile { get; set; }
        //public IFormFile AddressProofFile { get; set; }

        public AgencyDetails()
        {
            
        }
        public AgencyDetails(IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                populateList(this, reader);
            }
            else if (flag == 1)
            {
                populateObject(this, reader);
            }
        }

        private void populateObject(AgencyDetails agencyDetails, IDataReader reader)
        {
            throw new NotImplementedException();
        }

        private void populateList(AgencyDetails obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniquID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyName")))
            {
                obj.AgencyName = rdr.GetString(rdr.GetOrdinal("AgencyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CityName")))
            {
                obj.CityName = rdr.GetString(rdr.GetOrdinal("CityName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("StatusText")))
            {
                obj.IsActiveText = rdr.GetString(rdr.GetOrdinal("StatusText"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedBy")))
            {
                obj.CreatedBy = rdr.GetString(rdr.GetOrdinal("CreatedBy"));
            }
        }
    }
}
