using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace PhysioWeb.Models
{
    public class Users : CommanProp
    {

        public int UserId { get; set; }
        public string UserSerialNo { get; set; } = null;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public bool IsActive { get; set; }

        public int UserRoleId { get; set; }

        public string EmailId { get; set; }

        public Users()
        {
            
        }
        public Users(IDataReader data)
        {
            populateObject(this, data);
        }

        private void populateObject(Users obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserId")))
            {
                obj.UserId = rdr.GetInt32(rdr.GetOrdinal("UserId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserRoleId")))
            {
                obj.UserRoleId = rdr.GetInt32(rdr.GetOrdinal("UserRoleId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserSerialNo")))
            {
                obj.UserSerialNo = rdr.GetString(rdr.GetOrdinal("UserSerialNo"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserName")))
            {
                obj.UserName = rdr.GetString(rdr.GetOrdinal("UserName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("EmailId")))
            {
                obj.EmailId = rdr.GetString(rdr.GetOrdinal("EmailId"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Password")))
            {
                obj.Password = rdr.GetString(rdr.GetOrdinal("Password"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserRole")))
            {
                obj.UserRole = rdr.GetString(rdr.GetOrdinal("UserRole"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyId")))
            {
                obj.AgencyId = rdr.GetString(rdr.GetOrdinal("AgencyId"));
            }
            //if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            //{
            //    obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            //}

        }
    }
}
