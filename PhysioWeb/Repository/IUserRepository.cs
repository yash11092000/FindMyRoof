using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IUserRepository
    {
        Task<Users> Login(string username, string password, string email);
    }
}
