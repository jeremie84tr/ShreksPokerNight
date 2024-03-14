using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using toison;

static async Task main()
{
   //public string url { get; set; } = "http://10.120.29.149:7860/";
   var test = toison.Program.tester();
   await toison.html.PostAsync(html.sharedClient);
   Console.WriteLine(test);
}
main();

