
using System.Collections;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace PhysioWeb.Models
{
    public class HomeDashboard
    {
        public List<PropertyDetails> PropertyDetails { get; set; }
        public List<DropDownSource> PropertyCategoryList { get; set; }
        public List<DropDownSource> FurnishingTypeList { get; set; }
        public List<DropDownSource> PropertyTypeList { get; set; }
        public List<DropDownSource> RentalTypeList { get; set; }
        public List<DropDownSource> BedroomList { get; set; }
        public List<DropDownSource> AmenityList { get; set; }

        public int TotalCount { get; set; }
        public int CurrentPage{ get; set; }
        public HomeDashboard()
        {
            PropertyDetails = new List<PropertyDetails>();
            PropertyCategoryList = new List<DropDownSource>();
            PropertyTypeList = new List<DropDownSource>();
            FurnishingTypeList = new List<DropDownSource>();
            RentalTypeList = new List<DropDownSource>();
            BedroomList = new List<DropDownSource>();
            AmenityList = new List<DropDownSource>();
        }

    }

    public class PropertyDetails
    {
        public int PropertyId { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }

        public string LandMark { get; set; }

        public int BedRooms { get; set; }

        public int BathRooms { get; set; }

        public decimal Sqrtft { get; set; }

        public string PropertyName { get; set; }

        public string WhenListed { get; set; }

        public string PropertyImg { get; set; }

        public int ImageCount { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public string PropertyType { get; set; }
        public string Area { get; set; }

        public PropertyDetails(IDataReader Idr, int flag = 0)
        {
            if (flag == 0)
            {
                populateObject(this, Idr);
            }
            else if (flag == 1)
            {
                populateObjectAfterSearch(this, Idr);
            }
        }

        private void populateObjectAfterSearch(PropertyDetails obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquId")))
            {
                obj.PropertyId = rdr.GetInt32(rdr.GetOrdinal("UniquId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyName")))
            {
                obj.PropertyName = rdr.GetString(rdr.GetOrdinal("PropertyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("BedRooms")))
            {
                obj.BedRooms = rdr.GetInt32(rdr.GetOrdinal("BedRooms"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("BathRooms")))
            {
                obj.BathRooms = rdr.GetInt32(rdr.GetOrdinal("BathRooms"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MinPrice")))
            {
                obj.MinPrice = rdr.GetDecimal(rdr.GetOrdinal("MinPrice"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MaxPrice")))
            {
                obj.MaxPrice = rdr.GetDecimal(rdr.GetOrdinal("MaxPrice"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CarpetArea")))
            {
                obj.Sqrtft = rdr.GetDecimal(rdr.GetOrdinal("CarpetArea"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Address")))
            {
                obj.Address = rdr.GetString(rdr.GetOrdinal("Address"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LandMark")))
            {
                obj.LandMark = rdr.GetString(rdr.GetOrdinal("LandMark"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyType")))
            {
                obj.PropertyType = rdr.GetString(rdr.GetOrdinal("PropertyType"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Area")))
            {
                obj.Area = rdr.GetString(rdr.GetOrdinal("Area"));
            } 
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyImage")))
            {
                obj.PropertyImg = rdr.GetString(rdr.GetOrdinal("PropertyImage"));
            }
        }

        private void populateObject(PropertyDetails obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyId")))
            {
                obj.PropertyId = rdr.GetInt32(rdr.GetOrdinal("PropertyId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("BedRooms")))
            {
                obj.BedRooms = rdr.GetInt32(rdr.GetOrdinal("BedRooms"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ImageCount")))
            {
                obj.ImageCount = rdr.GetInt32(rdr.GetOrdinal("ImageCount"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("BathRooms")))
            {
                obj.BathRooms = rdr.GetInt32(rdr.GetOrdinal("BathRooms"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Price")))
            {
                obj.Price = rdr.GetDecimal(rdr.GetOrdinal("Price"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Sqrtft")))
            {
                obj.Sqrtft = rdr.GetDecimal(rdr.GetOrdinal("Sqrtft"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Address")))
            {
                obj.Address = rdr.GetString(rdr.GetOrdinal("Address"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LandMark")))
            {
                obj.LandMark = rdr.GetString(rdr.GetOrdinal("LandMark"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyName")))
            {
                obj.PropertyName = rdr.GetString(rdr.GetOrdinal("PropertyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("WhenListed")))
            {
                obj.WhenListed = rdr.GetString(rdr.GetOrdinal("WhenListed"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyImg")))
            {
                obj.PropertyImg = rdr.GetString(rdr.GetOrdinal("PropertyImg"));
            }
        }
    }
}
