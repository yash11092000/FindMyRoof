namespace PhysioWeb.Models
{
    public class PropertyMaster : CommanProp
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PropertyType { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal CarpetArea { get; set; }
        public decimal BuiltUpArea { get; set; }


        // Location
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        // Pricing
        public decimal Price { get; set; }
        public decimal? BudgetMin { get; set; }
        public decimal? BudgetMax { get; set; }


        // Features
        public List<string> Amenities { get; set; }
        public string FurnishingStatus { get; set; }
        public DateTime? PossessionDate { get; set; }

        public bool IsActive { get; set; }

    }
}
