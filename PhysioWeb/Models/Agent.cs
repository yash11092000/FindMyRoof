using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace PhysioWeb.Models
{
    public class Agent : CommanProp
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        public string ProfileImageFilePath { get; set; }
        public string ProfileImageFileName { get; set; }
        public string isActiveText { get; set; }

        public bool IsActive { get; set; } = true;
        public string Agency { get; set; }

        public IFormFile AgentProfile { get; set; }
        public string OldPassword { get; set; }
        public Agent()
        {

        }
        public Agent(IDataReader obj, int flag = 0)
        {
            if (flag == 0)
            {
                populateObjectList(this, obj);
            }
            else if (flag == 1)
            {
                populateObjectForEdit(this, obj);
            }
        }

        private void populateObjectForEdit(Agent obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniquID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("FirstName")))
            {
                obj.FirstName = rdr.GetString(rdr.GetOrdinal("FirstName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("MiddleName")))
            {
                obj.MiddleName = rdr.GetString(rdr.GetOrdinal("MiddleName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("LastName")))
            {
                obj.LastName = rdr.GetString(rdr.GetOrdinal("LastName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Email")))
            {
                obj.Email = rdr.GetString(rdr.GetOrdinal("Email"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Phone")))
            {
                obj.Phone = rdr.GetString(rdr.GetOrdinal("Phone"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AlternatePhone")))
            {
                obj.AlternatePhone = rdr.GetString(rdr.GetOrdinal("AlternatePhone"));
            }
            //if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyName")))
            //{
            //    obj.Agency = rdr.GetString(rdr.GetOrdinal("AgencyName"));
            //}
            if (!rdr.IsDBNull(rdr.GetOrdinal("ProfileImage")))
            {
                obj.ProfileImageFilePath = rdr.GetString(rdr.GetOrdinal("ProfileImage"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            {
                obj.IsActive = rdr.GetBoolean(rdr.GetOrdinal("IsActive"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserName")))
            {
                obj.UserName = rdr.GetString(rdr.GetOrdinal("UserName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Password")))
            {
                obj.Password = rdr.GetString(rdr.GetOrdinal("Password"));
            }
        }

        private void populateObjectList(Agent obj, IDataReader rdr)
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("UniquID")))
            {
                obj.UniquId = rdr.GetInt32(rdr.GetOrdinal("UniquID"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("FullName")))
            {
                obj.FirstName = rdr.GetString(rdr.GetOrdinal("FullName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Email")))
            {
                obj.Email = rdr.GetString(rdr.GetOrdinal("Email"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("Phone")))
            {
                obj.Phone = rdr.GetString(rdr.GetOrdinal("Phone"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("AgencyName")))
            {
                obj.Agency = rdr.GetString(rdr.GetOrdinal("AgencyName"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("ProfileImage")))
            {
                obj.ProfileImageFilePath = rdr.GetString(rdr.GetOrdinal("ProfileImage"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("IsActive")))
            {
                obj.isActiveText = rdr.GetString(rdr.GetOrdinal("IsActive"));
            }
            if (!rdr.IsDBNull(rdr.GetOrdinal("CreatedBy")))
            {
                obj.CreatedBy = rdr.GetString(rdr.GetOrdinal("CreatedBy"));
            }
        }
    }

}
