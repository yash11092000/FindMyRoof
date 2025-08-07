
using System.Data;

namespace PhysioWeb.Models
{
    public class HomeDashboard
    {
        public List<PropertyDetails> PropertyDetails { get; set; }
        public HomeDashboard()
        {
            PropertyDetails = new List<PropertyDetails>();
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

        public PropertyDetails(IDataReader Idr)
        {
            populateObject(this, Idr);
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
