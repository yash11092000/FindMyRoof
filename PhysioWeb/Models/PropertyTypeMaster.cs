using System.Data;

namespace PhysioWeb.Models
{
    public class PropertyTypeMaster : CommanProp
    {
        public string PropertyType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string InActiveText { get; set; }
        public PropertyTypeMaster() { }

        public PropertyTypeMaster(IDataReader reader)
        {
           
            TotalCount = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
            UniquId = Convert.ToInt32(reader["UniquId"]);
            PropertyType = reader["PropertyType"].ToString();
            InActiveText = reader["IsActive"].ToString();
            Description = reader["Description"].ToString();
            CreatedBy = reader["CreatedBy"].ToString();
        }
    }

}
