using System.Reflection;
using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbHelper _dbHelper;

        public UserRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<Users> Login(string Email, string Mobile, string Password)
        {
            try
            {
                string[] parametersName = { "Email", "Password", "Mobile" };
                object[] Values = { Email, Password, Mobile };

                string Sp = "FMR_CheckUser";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);
                while (data.Read())
                {
                    Users users = new Users(data);
                    return users;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> RegisterUser(Register register)
        {
            try
            {
                string[] parametersName = { "Name", "Email", "Mobile", "Password", "Role" };
                object[] Values = { register.Name, register.Email, register.Mobile, register.Password, 4 };

                string Sp = "FMR_SaveUser";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<HomeDashboard> GetDashboardData()
        {
            try
            {
                string[] parametersName = { };
                object[] Values = { };

                string Sp = "FMR_GetDashboardData";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);
                HomeDashboard home = new HomeDashboard();
                while (data.Read())
                {
                    home.PropertyDetails.Add(new PropertyDetails(data));

                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.PropertyTypeList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.BedroomList.Add(new DropDownSource(data, true));
                    }
                }
                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.AmenityList.Add(new DropDownSource(data, true));
                    }
                }

                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.RentalTypeList.Add(new DropDownSource(data, true));
                    }
                }

                if (data.NextResult())
                {
                    while (data.Read())
                    {
                        home.PropertyCategoryList.Add(new DropDownSource(data, true));
                    }
                }
                return home;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //public async Task<PropertyMaster> GetPropertyDetails()
        //{
        //    try
        //    {
        //        string[] parameterNames = { };
        //        object[] parameterValues = { };


        //        string Sp = "FMR_GetPropertyDetails";
        //        var data = await _dbHelper.GetDataReaderAsync(Sp, parameterNames, parameterValues);
        //        PropertyMaster propertyMaster = new PropertyMaster();

               


        //        return propertyMaster;


        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    }
}
