namespace PhysioWeb.Models
{
    public class AmenityMaster : CommanProp
    {
        public string AmenityName { get; set; }
        public string IconImage { get; set; } 
        public bool IsActive { get; set; }
        public string InActiveText { get; set; }
        public AmenityMaster()
        {
        }
        public AmenityMaster(System.Data.IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                UniquId = Convert.ToInt32(reader["UniquId"]);
                AmenityName = reader["AmenityName"].ToString();
                InActiveText = reader["IsActive"].ToString();

                CreatedBy = reader["CreatedBy"].ToString();
            }
            else if (flag == 1)
            {
                populateObject(this, reader);
            }
        }
        private void populateObject(AmenityMaster obj, System.Data.IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniqueID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniqueID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AmenityName")))
            {
                obj.AmenityName = rdr.GetString(rdr.GetOrdinal("AmenityName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("IconImage")))
            {
                obj.IconImage = rdr.GetString(rdr.GetOrdinal("IconImage"));
            }

            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            {
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            }

        }
    }
}
