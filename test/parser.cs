using System.Text.Json;
using System.Text;
using System.Net.Http;


namespace toison
{
    public class messa
    {
        public string? role { get; set; }
        public string? content { get; set; }
    }
    public class demande
    {
        public List<messa>? message { get; set; }

        public string? mode { get; set; }
        public string? instruction_template { get; set; }
    }

    public class Program
    {
        public static StringContent tester()
        {
            var mess = new messa
            {
                role = "user",
                content = "How are you ?"
            };
            var demande = new demande
            {
                message = new List<messa>()
                 {mess},
                mode = "instruct",
                instruction_template = "Alpaca"
            };
            //var options = new JsonSerializerOptions { WriteIndented = true };
            using StringContent jsonString = new(JsonSerializer.Serialize(demande /*,option*/),Encoding.UTF8,"application/json");
            return jsonString;
        }
    }
    public class html
{
    public static HttpClient sharedClient = new()
    {
        //BaseAddress = new Uri("http://10.120.29.149:5000"),
        BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
    };

    public static async Task PostAsync(HttpClient httpClient)
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
        Console.WriteLine("coucou");
        Console.WriteLine($"{jsonContent}\n");
        using HttpResponseMessage reponse = await httpClient.PostAsync("todos", jsonContent);
        Console.WriteLine(reponse);
        Console.WriteLine("coucou2");
        var truc = reponse.EnsureSuccessStatusCode();
        Console.WriteLine(truc);
        var jsonResponse = await reponse.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }
}

}