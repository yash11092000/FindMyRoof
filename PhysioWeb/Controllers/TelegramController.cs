using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        #region telegram integration
        private readonly ITelegramServices _telegramservice;

        public TelegramController(ITelegramServices telegramServices)
        {
            _telegramservice = telegramServices;    
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] JsonElement update)
        {
            try
            {
                // Extract chat id and message text
                var message = update.GetProperty("message");
                var chat = message.GetProperty("chat");
                var chatId = chat.GetProperty("id").GetInt64();
                var userName = message.GetProperty("from").GetProperty("first_name").GetString();

                Console.WriteLine($"User: {userName}, ChatID: {chatId}");

                // You can save this chatId to DB here
                // await _userService.SaveChatId(userName, chatId);
                await _telegramservice.SaveChatId(userName, chatId);

                // Reply to user (optional)
                await SendMessageAsync(chatId.ToString(), "👋 Thanks for connecting with us!");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing update: {ex.Message}");
                return BadRequest();
            }
        }

        private async Task SendMessageAsync(string chatId, string message)
        {
            var botToken = "8360265527:AAFI_9icXKVLxHKl13hjr1YfQsOGEcmXn_4";
            var url = $"https://api.telegram.org/bot{botToken}/sendMessage";

            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("chat_id", chatId),
                new KeyValuePair<string, string>("text", message)
            });

            await client.PostAsync(url, content);
        }


        #endregion
    }
}
