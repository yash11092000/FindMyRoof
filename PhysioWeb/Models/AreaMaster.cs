using System;
using System.Collections.Generic;
using System.Data;

namespace PhysioWeb.Models
{
    public class AreaMaster : CommanProp
    {
        public string AreaName { get; set; }
        public string Pincode { get; set; }
        public string SubAreaName { get; set; }
        public bool IsActive { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public string State { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }

        public List<DropDownSource> StateList { get; set; }
        public List<DropDownSource> CityList { get; set; }
        public List<DropDownSource> CountryList { get; set; }
        public string InActiveText { get; set; }

        public AreaMaster() { }

        public AreaMaster(IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                UniquId = reader["UniquId"] != DBNull.Value ? Convert.ToInt32(reader["UniquId"]) : 0;
                AreaName = reader["AreaName"]?.ToString();
                SubAreaName = reader["SubAreaName"]?.ToString();
                City = reader["CityName"]?.ToString();
                InActiveText = reader["IsActive"].ToString();
                CreatedBy = reader["CreatedBy"]?.ToString();
            }
            else if (flag == 1)
            {
                populateObject(this, reader);
            }
        }

        private void populateObject(AreaMaster obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniqueID")))
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniqueID"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("AreaName")))
                obj.AreaName = rdr.GetString(rdr.GetOrdinal("AreaName"));


            if (!rdr.IsDBNull(rdr.GetOrdinal("SubAreaName")))
                obj.SubAreaName = rdr.GetString(rdr.GetOrdinal("SubAreaName"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("Pincode")))
                obj.Pincode = rdr.GetString(rdr.GetOrdinal("Pincode"));


            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("CityID")))
                obj.CityID = rdr.GetInt32(rdr.GetOrdinal("CityID"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("StateID")))
                obj.StateID = rdr.GetInt32(rdr.GetOrdinal("StateID"));

            if (!rdr.IsDBNull(rdr.GetOrdinal("CountryID")))
                obj.CountryID = rdr.GetInt32(rdr.GetOrdinal("CountryID"));
        }
    }
}
