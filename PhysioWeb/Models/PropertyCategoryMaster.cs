using System.Data;

namespace PhysioWeb.Models
{
    public class PropertyCategoryMaster : CommanProp
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public string InActiveText { get; set; }
        public PropertyCategoryMaster()
        {

        }

        public PropertyCategoryMaster(IDataReader reader, int flag = 0)
        {
            if (flag == 0)
            {
                TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                UniquId = Convert.ToInt32(reader["UniquId"]);
                CategoryName = reader["PropertyCategoryName"].ToString();
                InActiveText = reader["IsActive"].ToString();
                CreatedBy = reader["CreatedBy"].ToString();
            }
            else if (flag == 1)
            {
                populateObject(this, reader);
            }
        }
        private void populateObject(PropertyCategoryMaster obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniqueID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniqueID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("PropertyCategoryName")))
            {
                obj.CategoryName = rdr.GetString(rdr.GetOrdinal("PropertyCategoryName"));
            }
            
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            {
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            }

        }
    }

}
