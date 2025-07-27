using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IUserRepository
    {
        Task<Users> Login(string Email, string Mobile, string Password);
        Task<bool> RegisterUser(Register register);
    }
}
