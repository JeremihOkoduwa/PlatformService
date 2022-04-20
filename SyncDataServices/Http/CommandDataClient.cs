using System.Text;
using System.Net.Http;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class CommandDataClient : ICommandDataClient
    {
        readonly HttpClient _client;
        readonly IConfiguration _configuration;
        public CommandDataClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto request)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(request),Encoding.UTF8,"application/json");
            var response = await _client.PostAsync(_configuration["CommandsService"], httpContent);

            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine("response: {0}", value);
            }
            else
            {
                System.Console.WriteLine("failed to post");
            }
        }
    }
}