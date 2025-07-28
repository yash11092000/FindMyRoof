using System.Data;

namespace PhysioWeb.Models
{
    public class PropertyTypeMaster : CommanProp
    {
        public string PropertyType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string InActiveText { get; set; }
       
        public PropertyTypeMaster(IDataReader reader , int flag = 0)
        {
            if (flag == 0)
            {
                TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                UniquId = Convert.ToInt32(reader["UniquId"]);
                PropertyType = reader["PropertyType"].ToString();
                InActiveText = reader["IsActive"].ToString();
                Description = reader["Description"].ToString();
                CreatedBy = reader["CreatedBy"].ToString();
            }
            else if (flag == 1) {
                populateObject(this, reader);
            }
        }
        private void populateObject(PropertyTypeMaster obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniqueID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniqueID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyType")))
            {
                obj.PropertyType = rdr.GetString(rdr.GetOrdinal("PropertyType"));
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
