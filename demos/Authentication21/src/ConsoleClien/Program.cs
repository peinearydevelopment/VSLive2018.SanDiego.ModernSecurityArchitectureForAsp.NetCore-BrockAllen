using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ConsoleClien
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            var tokenUrl = "http://localhost:5000/connect/token";
            var tokenClient = new TokenClient(tokenUrl, "console", "secret");
            var tokenResult = await tokenClient.RequestClientCredentialsAsync("api1");
            if (tokenResult.IsError)
            {
                Console.WriteLine($"token error {tokenResult.Error}");
                return;
            }

            Console.WriteLine($"Token Result:");
            Console.WriteLine(tokenResult.Raw);
            Console.WriteLine();

            var url = "http://localhost:2284/test";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);

            var result = await client.GetAsync(url);
            Console.WriteLine($"Test result {(int)result.StatusCode}");

            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                Console.WriteLine(json);
            }
        }
    }
}
