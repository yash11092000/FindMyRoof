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

        // ✅ For Handling Uploads in Forms
        //public IFormFile AgencyLogoFile { get; set; }
        //public IFormFile ReraCertificateFile { get; set; }
        //public IFormFile AgencyLicenseFile { get; set; }
        //public IFormFile AddressProofFile { get; set; }
    }
}
