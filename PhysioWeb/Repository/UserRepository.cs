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
                object[] Values = { Email, Password, Mobile};

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
    }
}
