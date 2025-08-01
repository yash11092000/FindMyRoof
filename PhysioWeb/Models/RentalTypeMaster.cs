namespace PhysioWeb.Models
{
    public class RentalTypeMaster : CommanProp
    {
        public string RentalType { get; set; }
        public string Description { get; set; }
        public string InActiveText { get; set; }
        public bool IsActive { get; set; }

        public RentalTypeMaster()
        {

        }
        public RentalTypeMaster(System.Data.IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                UniquId = Convert.ToInt32(reader["UniquId"]);
                RentalType = reader["RentalType"].ToString();
                InActiveText = reader["IsActive"].ToString();
                Description = reader["Description"].ToString();
                CreatedBy = reader["CreatedBy"].ToString();
            }
            else if (flag == 1)
            {
                populateObject(this, reader);
            }
        }
        private void populateObject(RentalTypeMaster obj, System.Data.IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniqueID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniqueID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("RentalType")))
            {
                obj.RentalType = rdr.GetString(rdr.GetOrdinal("RentalType"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Description")))
            {
                obj.Description = rdr.GetString(rdr.GetOrdinal("Description"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            {
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            }

        }
    }
}
