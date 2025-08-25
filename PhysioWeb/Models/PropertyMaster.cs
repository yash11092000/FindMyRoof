using System;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using Microsoft.Identity.Client;

namespace PhysioWeb.Models
{
    public class PropertyMaster : CommanProp
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PropertyType { get; set; }
        public string TransactionType { get; set; }
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
        public string Amenities { get; set; }
        public string FurnishingStatus { get; set; }
        public DateTime? PossessionDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsNegotiable { get; set; }


        //need to add
        public string ContactPersonName { get; set; }

        public string ContactPersonPhone { get; set; }
        public string ContactPersonAlternatePhone { get; set; }

        public string Area { get; set; }

        public string SubArea { get; set; }

        public string Country { get; set; }

        public string Floor { get; set; }

        public string PropertyCategory { get; set; }
        public string CityText { get; set; }

        public List<DropDownSource> CountryList { get; set; }
        public List<DropDownSource> StateList { get; set; }
        public List<DropDownSource> CityList { get; set; }
        public List<DropDownSource> PropertyCategoryList { get; set; }
        public List<DropDownSource> AreaList { get; set; }

        public decimal SecurityDeposit { get; set; }
        public string InActiveText { get; set; }

        public List<DropDownSource> FurnishingTypeList { get; set; }
        public List<DropDownSource> PropertyTypeList { get; set; }
        public List<DropDownSource> RentalTypeList { get; set; }
        public List<DropDownSource> BedRoomList { get; set; }
        public List<DropDownSource> AmenityList { get; set; }

        public List<DropDownSource> Images { get; set; }

        public List<DropDownSource> Videos { get; set; }

        public List<DropDownSource> Amenitie { get; set; }

        public string Vastu { get; set; }
        public int YearOfConstruction { get; set; }
        public int TotalFloorBuilding { get; set; }
        public List<DropDownSource> AmountUnitList { get; set; }
        public List<DropDownSource> PreferedBuyerTypeList { get; set; }

        public string DisplayMaxPrice { get; set; }

        public string DisplayMinPrice { get; set; }
        public string PreferedBuyerType { get; set; }
        public string AmountUnitMinPrice { get; set; }
        public string AmountUnitMaxPrice { get; set; }
        public decimal ConvertedActualPrice { get; set; }
        public decimal ConvertedNegotiablePrice { get; set; }
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
            AmenityList = new List<DropDownSource>();
            Amenitie = new List<DropDownSource>();
            AmountUnitList = new List<DropDownSource>();
            Videos = new List<DropDownSource>();
            Images = new List<DropDownSource>();
            PreferedBuyerTypeList = new List<DropDownSource>();
        }

        public PropertyMaster(IDataReader Idr, int flag)
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
            AmenityList = new List<DropDownSource>();
            Amenitie = new List<DropDownSource>();
            AmountUnitList = new List<DropDownSource>();
            Videos = new List<DropDownSource>();
            Images = new List<DropDownSource>();
            PreferedBuyerTypeList = new List<DropDownSource>();


            if (flag == 1)
            {
                populateObject2(this, Idr);
            }
            else if (flag == 2)
            {
                populateObjectList(this, Idr);
            }
            else if (flag == 3)
            {
                populateObjectEdit(this, Idr);
            }
            else
            {
                populateObject(this, Idr);
            }
        }
        private void populateObjectList(PropertyMaster obj, IDataReader rdr)
        {
            TotalCount = rdr["TotalCount"] != DBNull.Value ? Convert.ToInt32(rdr["TotalCount"]) : 0;
            UniquId = Convert.ToInt32(rdr["UniqueId"]);
            Title = rdr["PropertyName"].ToString();
            PropertyType = rdr["PropertyType"].ToString();
            TransactionType = rdr["RentalType"].ToString();
            CityText = rdr["CityName"].ToString();
            ContactPersonName = rdr["ContactPersonName"].ToString();
            InActiveText = rdr["IsActive"].ToString();
            CreatedBy = rdr["CreatedBy"].ToString();
        }

        private void populateObject2(PropertyMaster obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("SubAreaName")))
            {
                obj.SubArea = rdr.GetString(rdr.GetOrdinal("SubAreaName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CityID")))
            {
                obj.City = rdr.GetString(rdr.GetOrdinal("CityID"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("StateID")))
            {
                obj.State = rdr.GetString(rdr.GetOrdinal("StateID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CountryID")))
            {
                obj.Country = rdr.GetString(rdr.GetOrdinal("CountryID"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("PinCode")))
            {
                obj.PinCode = rdr.GetString(rdr.GetOrdinal("PinCode"));
            }

        }
        private void populateObject(PropertyMaster obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquId")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniquId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyName")))
            {
                obj.Title = rdr.GetString(rdr.GetOrdinal("PropertyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Description")))
            {
                obj.Description = rdr.GetString(rdr.GetOrdinal("Description"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyType")))
            {
                obj.PropertyType = rdr.GetString(rdr.GetOrdinal("PropertyType"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("TransactionType")))
            {
                obj.TransactionType = rdr.GetString(rdr.GetOrdinal("TransactionType"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("Bedrooms")))
            {
                obj.Bedrooms = rdr.GetInt32(rdr.GetOrdinal("Bedrooms"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("Bathrooms")))
            {
                obj.Bathrooms = rdr.GetInt32(rdr.GetOrdinal("Bathrooms"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("CarpetArea")))
            {
                obj.CarpetArea = rdr.GetDecimal(rdr.GetOrdinal("CarpetArea"));
            }

            //if (!rdr.IsDBNull(rdr.GetOrdinal("BuiltUpArea")))
            //{
            //    obj.BuiltUpArea = rdr.GetDecimal(rdr.GetOrdinal("BuiltUpArea"));
            //}

            if (!rdr.IsDBNull(rdr.GetOrdinal("Address")))
            {
                obj.Address = rdr.GetString(rdr.GetOrdinal("Address"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("City")))
            {
                obj.City = rdr.GetString(rdr.GetOrdinal("City"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("State")))
            {
                obj.State = rdr.GetString(rdr.GetOrdinal("State"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Country")))
            {
                obj.Country = rdr.GetString(rdr.GetOrdinal("Country"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("PinCode")))
            {
                obj.PinCode = rdr.GetString(rdr.GetOrdinal("PinCode"));
            }

            //if (!rdr.IsDBNull(rdr.GetOrdinal("Latitude")))
            //{
            //    obj.Latitude = rdr.GetDecimal(rdr.GetOrdinal("Latitude"));
            //}

            //if (!rdr.IsDBNull(rdr.GetOrdinal("Longitude")))
            //{
            //    obj.Longitude = rdr.GetDecimal(rdr.GetOrdinal("Longitude"));
            //}

            if (!rdr.IsDBNull(rdr.GetOrdinal("Landmark")))
            {
                obj.Landmark = rdr.GetString(rdr.GetOrdinal("Landmark"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("BudgetMin")))
            {
                obj.BudgetMin = rdr.GetDecimal(rdr.GetOrdinal("BudgetMin"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("BudgetMax")))
            {
                obj.BudgetMax = rdr.GetDecimal(rdr.GetOrdinal("BudgetMax"));
            }

            //if (!rdr.IsDBNull(rdr.GetOrdinal("Amenities")))
            //{
            //    obj.Amenities = rdr.GetString(rdr.GetOrdinal("Amenities"));
            //}

            if (!rdr.IsDBNull(rdr.GetOrdinal("FurnishingStatus")))
            {
                obj.FurnishingStatus = rdr.GetString(rdr.GetOrdinal("FurnishingStatus"));
            }

            //if (!rdr.IsDBNull(rdr.GetOrdinal("PossessionDate")))
            //{
            //    obj.PossessionDate = rdr.GetDateTime(rdr.GetOrdinal("PossessionDate"));
            //}

            //if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            //{
            //    obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            //}

            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactPersonName")))
            {
                obj.ContactPersonName = rdr.GetString(rdr.GetOrdinal("ContactPersonName"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactPersonPhone")))
            {
                obj.ContactPersonPhone = rdr.GetString(rdr.GetOrdinal("ContactPersonPhone"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactPersonAlternatePhone")))
            {
                obj.ContactPersonAlternatePhone = rdr.GetString(rdr.GetOrdinal("ContactPersonAlternatePhone"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("Area")))
            {
                obj.Area = rdr.GetString(rdr.GetOrdinal("Area"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("SubArea")))
            {
                obj.SubArea = rdr.GetString(rdr.GetOrdinal("SubArea"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("Country")))
            {
                obj.Country = rdr.GetString(rdr.GetOrdinal("Country"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("Floor")))
            {
                obj.Floor = rdr.GetString(rdr.GetOrdinal("Floor"));
            }

            //if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyCategory")))
            //{
            //    obj.PropertyCategory = rdr.GetString(rdr.GetOrdinal("PropertyCategory"));
            //}
        }

        private void populateObjectEdit(PropertyMaster obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquId")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniquId"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyName")))
            {
                obj.Title = rdr.GetString(rdr.GetOrdinal("PropertyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyType")))
            {
                obj.PropertyType = rdr.GetString(rdr.GetOrdinal("PropertyType"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Bedroom")))
            {
                obj.Bedrooms = rdr.GetInt32(rdr.GetOrdinal("Bedroom"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyCategory")))
            {
                obj.PropertyCategory = rdr.GetString(rdr.GetOrdinal("PropertyCategory"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("TransactionType")))
            {
                obj.TransactionType = rdr.GetString(rdr.GetOrdinal("TransactionType"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Bathroom")))
            {
                obj.Bathrooms = rdr.GetInt32(rdr.GetOrdinal("Bathroom"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertySize")))
            {
                obj.CarpetArea = rdr.GetDecimal(rdr.GetOrdinal("PropertySize"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("Floor")))
            {
                obj.Floor = rdr.GetString(rdr.GetOrdinal("Floor"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("TotalFloorBuilding")))
            {
                obj.TotalFloorBuilding = rdr.GetInt32(rdr.GetOrdinal("TotalFloorBuilding"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("FurnishingStatus")))
            {
                obj.FurnishingStatus = rdr.GetString(rdr.GetOrdinal("FurnishingStatus"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Vastu")))
            {
                obj.Vastu = rdr.GetString(rdr.GetOrdinal("Vastu"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("PreferedBuyerType")))
            {
                obj.PreferedBuyerType = rdr.GetString(rdr.GetOrdinal("PreferedBuyerType"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("YearOfConstruction")))
            {
                obj.YearOfConstruction = rdr.GetInt32(rdr.GetOrdinal("YearOfConstruction"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyDescription")))
            {
                obj.Description = rdr.GetString(rdr.GetOrdinal("PropertyDescription"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactPersonName")))
            {
                obj.ContactPersonName = rdr.GetString(rdr.GetOrdinal("ContactPersonName"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactPersonNo")))
            {
                obj.ContactPersonPhone = rdr.GetString(rdr.GetOrdinal("ContactPersonNo"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("ContactPersonAlternatePhone")))
            {
                obj.ContactPersonAlternatePhone = rdr.GetString(rdr.GetOrdinal("ContactPersonAlternatePhone"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("Area")))
            {
                obj.Area = rdr.GetString(rdr.GetOrdinal("Area"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("SubArea")))
            {
                obj.SubArea = rdr.GetString(rdr.GetOrdinal("SubArea"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PinCode")))
            {
                obj.PinCode = rdr.GetString(rdr.GetOrdinal("PinCode"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("City")))
            {
                obj.City = rdr.GetString(rdr.GetOrdinal("City"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("State")))
            {
                obj.State = rdr.GetString(rdr.GetOrdinal("State"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Country")))
            {
                obj.Country = rdr.GetString(rdr.GetOrdinal("Country"));
            }


            if (!rdr.IsDBNull(rdr.GetOrdinal("Address")))
            {
                obj.Address = rdr.GetString(rdr.GetOrdinal("Address"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("Landmark")))
            {
                obj.Landmark = rdr.GetString(rdr.GetOrdinal("Landmark"));
            }
            
            if (!rdr.IsDBNull(rdr.GetOrdinal("MinPrice")))
            {
                obj.BudgetMin = rdr.GetDecimal(rdr.GetOrdinal("MinPrice"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("MaxPrice")))
            {
                obj.BudgetMax = rdr.GetDecimal(rdr.GetOrdinal("MaxPrice"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("AmountUnitMinPrice")))
            {
                obj.AmountUnitMinPrice = rdr.GetString(rdr.GetOrdinal("AmountUnitMinPrice"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("AmountUnitMaxPrice")))
            {
                obj.AmountUnitMaxPrice = rdr.GetString(rdr.GetOrdinal("AmountUnitMaxPrice"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("ConvertedActualPrice")))
            {
                obj.ConvertedActualPrice = rdr.GetDecimal(rdr.GetOrdinal("ConvertedActualPrice"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("ConvertedNegotiablePrice")))
            {
                obj.ConvertedNegotiablePrice = rdr.GetDecimal(rdr.GetOrdinal("ConvertedNegotiablePrice"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("IsNegotiable")))
            {
                obj.IsNegotiable = rdr.GetBoolean(rdr.GetOrdinal("IsNegotiable"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            {
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            }
        }

    }
}
