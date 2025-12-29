using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{

    public class TelegramServices : ITelegramServices
    {

        private readonly DbHelper _dbHelper;

        public TelegramServices(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<bool> SaveChatId(string? userName, long chatId)
        {
            try
            {
                string[] parametersName = { "UserName", "ChatId" };
                object[] Values = { userName, chatId };

                string Sp = "SaveTelegramUser";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
    }
}

