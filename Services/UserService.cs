using Products_service.DTOS;
using System.Text.Json;

namespace Products_service.Services
{
    public class UserService
    {
        private HttpClient client;
        public UserService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<UserDTO?> GetUserAsync(int adminId)
        {
            try
            {
                var url = $"https://localhost:7209/user/index/{adminId}";
                var responsne = await client.GetAsync(url);
                if (responsne.IsSuccessStatusCode)
                {
                    var json = await responsne.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<UserDTO>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return user;
                }
                else
                {
                    return null;
                }
            }
             catch
            {
                return null;
            }
        }
    }
}
