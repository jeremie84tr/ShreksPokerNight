using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace test
{
    public class test
    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com")//https://jsonplaceholder.typicode.com
        };
        static async Task PostAsync(HttpClient httpClient)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    userId = 77,
                    id = 1,
                    title = "write code sample",
                    completed = false
                }),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = await httpClient.PostAsync(
                "todos",
                jsonContent);

            response.EnsureSuccessStatusCode();
                /*.WriteRequestToConsole();*/
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");

            // Expected output:
            //   POST https://jsonplaceholder.typicode.com/todos HTTP/1.1
            //   {
            //     "userId": 77,
            //     "id": 201,
            //     "title": "write code sample",
            //     "completed": false
            //   }
        }
        public static async Task Main()
        {
            await PostAsync(sharedClient);
        }
    }
}