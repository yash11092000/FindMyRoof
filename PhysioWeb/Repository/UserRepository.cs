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
        public async Task<Users> Login(string username, string password, string email)
        {
            string[] parametersName = { "UserName", "Password", "Email" };
            object[] Values = { username, password, email };

            string Sp = "FMR_CheckUser";
            var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);
            while (data.Read())
            {
                Users users = new Users(data);

            }
            return null;

        }
    }
}
