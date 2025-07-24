namespace PhysioWeb.Models
{
    public class AgentsDetails
    {
        public string AgentType { get; set; }
        public string FirmName { get; set; }
        public string ContactPersonName { get; set; }
        public string Photo { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }

        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateContactNumber { get; set; }

        public int AgencyID { get; set; }
        public string Position { get; set; }
        public string YearsOfExperience { get; set; }

        public string CurrentAddress { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public string Pincode { get; set; }
        public int CountryID { get; set; }

        public string ReraRegistrationNumber { get; set; }
        public string IDProofType { get; set; }
        public string IDProofNumber { get; set; }
        public string IDProof { get; set; }
        public string ReraCertificate { get; set; }

        public int PropertiesHandled { get; set; } = 0;
        public int ActiveListingsCount { get; set; } = 0;
        public int TotalSalesDone { get; set; } = 0;
        public decimal CustomerRating { get; set; } = 0;

        public string WorkingHours { get; set; }
        public string AvailabilityDays { get; set; }
        public string PreferredLocations { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
