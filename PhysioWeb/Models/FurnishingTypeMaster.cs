using System.Data;

namespace PhysioWeb.Models
{
    public class FurnishingTypeMaster : CommanProp
    {
       
        public string FurnishingType { get; set; }
        public bool IsActive { get; set; }
        public string InActiveText { get; set; }
        public FurnishingTypeMaster()
        {

        }
        public FurnishingTypeMaster(IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                UniquId = Convert.ToInt32(reader["UniquId"]);
                FurnishingType = reader["FurnishingType"].ToString();
                InActiveText = reader["IsActive"].ToString();
               
                CreatedBy = reader["CreatedBy"].ToString();
            }
            else if (flag == 1)
            {
                populateObject(this, reader);
            }
        }
        private void populateObject(FurnishingTypeMaster obj, System.Data.IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniqueID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniqueID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("FurnishingType")))
            {
                obj.FurnishingType = rdr.GetString(rdr.GetOrdinal("FurnishingType"));
            }
           
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            {
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            }

        }
    }
}
