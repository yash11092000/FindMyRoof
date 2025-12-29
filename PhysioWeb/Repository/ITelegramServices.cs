
namespace PhysioWeb.Repository
{
    public interface ITelegramServices
    {
        Task<bool> SaveChatId(string? userName, long chatId);
    }
}
