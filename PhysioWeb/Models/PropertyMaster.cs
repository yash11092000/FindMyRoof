using Microsoft.Identity.Client;

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

        public string Landmark { get; set; }


        // Pricing
        // public decimal Price { get; set; }
        public decimal? BudgetMin { get; set; }
        public decimal? BudgetMax { get; set; }


        // Features
        public List<string> Amenities { get; set; }
        public string FurnishingStatus { get; set; }
        public DateTime? PossessionDate { get; set; }

        public bool IsActive { get; set; }


        //need to add
        public string ContactPersonName { get; set; }

        public string ContactPersonPhone { get; set; }
        public string ContactPersonAlternatePhone { get; set; }

        public string Area { get; set; }

        public string SubArea { get; set; }

        public int Country { get; set; }

        public string Floor { get; set; }

        public string PropertyCategory { get; set; }

        public List<DropDownSource> CountryList { get; set; }
        public List<DropDownSource> StateList { get; set; }
        public List<DropDownSource> CityList { get; set; }
        public List<DropDownSource> PropertyCategoryList { get; set; }
        public List<DropDownSource> AreaList { get; set; }

        public decimal SecurityDeposit { get; set; }

        public List<DropDownSource> FurnishingTypeList { get; set; }
        public List<DropDownSource> PropertyTypeList { get; set; }
        public List<DropDownSource> RentalTypeList { get; set; }
        public List<DropDownSource> BedRoomList { get; set; }

        public PropertyMaster()
        {
            CountryList = new List<DropDownSource>();
            StateList = new List<DropDownSource>();
            CityList = new List<DropDownSource>();
            PropertyCategoryList = new List<DropDownSource>();
            AreaList = new List<DropDownSource>();
            PropertyTypeList = new List<DropDownSource>();
            FurnishingTypeList = new List<DropDownSource>();
            RentalTypeList = new List<DropDownSource>();
            BedRoomList = new List<DropDownSource>();
        }


    }
}
