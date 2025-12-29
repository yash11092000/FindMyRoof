using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PhysioWeb.Repository
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly string _phoneNumberId;
        private readonly string _accessToken;
        private readonly string _recipientNumber;

        public WhatsAppService(IConfiguration configuration)
        {
            _phoneNumberId = configuration["WhatsAppConfig:PhoneNumberId"];
            _accessToken = configuration["WhatsAppConfig:AccessToken"];
            _recipientNumber = configuration["WhatsAppConfig:RecipientNumber"];
        }
        public async Task<string> SendImageAsync(string toPhoneNumber, string uploadResponse)
        {
            string mediaId = JObject.Parse(uploadResponse)["id"]?.ToString();
            if (string.IsNullOrEmpty(mediaId))
                throw new Exception("Invalid media ID received from upload response.");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var payload = new
                {
                    messaging_product = "whatsapp",
                    to = toPhoneNumber,
                    type = "image",
                    image = new { id = mediaId }
                };
                var jsonPayload = JsonConvert.SerializeObject(payload);

                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"https://graph.facebook.com/v22.0/{_phoneNumberId}/messages", content);

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                return json;
            }
        }
        public async Task<string> UploadImageAsync(string localFilePath)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                using (var form = new MultipartFormDataContent())
                {
                    var imageContent = new ByteArrayContent(File.ReadAllBytes(localFilePath));
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                    form.Add(imageContent, "file", Path.GetFileName(localFilePath));
                    form.Add(new StringContent("whatsapp"), "messaging_product");

                    var response = await client.PostAsync($"https://graph.facebook.com/v22.0/{_phoneNumberId}/media", form);
                    var json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(json);
                    return json;
                }
            }
        }
    }
}
