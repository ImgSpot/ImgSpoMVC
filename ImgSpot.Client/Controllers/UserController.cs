using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImgSpot.Client.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.IO;
using System.Net.Http.Json;

namespace ImgSpot.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private static readonly string SubscriptionKey = "dfda2680db5a40fbb5a778cff852de54";
        private static readonly string Endpoint = "https://eastus.api.cognitive.microsoft.com/contentmoderator/moderate/v1.0/ProcessText/Screen?classify=True";
        public static readonly string ResultsFile = "res.txt";

        public static ContentModeratorClient Authenticate(string key, string endpoint)
        {
            ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key));
            client.Endpoint = endpoint;

            return client;
        }

        public static void ModerateText(ContentModeratorClient client, string inputText, string output)
        {
            string text = inputText;
            text = text.Replace(Environment.NewLine, " ");
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream(textBytes);
            Console.WriteLine("Screening {0}...", inputText);

            using (StreamWriter outputWriter = new StreamWriter(output, false))
            {
                using (client)
                {
                    outputWriter.WriteLine("Autocorrect typos, check for matching terms, PII, and classify.");

                    var screenResult = client.TextModeration.ScreenText("text/plain", stream, "eng", true, true, null, true);
                    outputWriter.WriteLine(JsonConvert.SerializeObject(screenResult, Formatting.Indented));
                }
                outputWriter.Flush();
                outputWriter.Close();
            }
            Console.WriteLine("Results written to {0}", output);
            Console.WriteLine();
        }

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserModel uploadedImage)
        {
            ContentModeratorClient clientText = Authenticate(SubscriptionKey, Endpoint);
            ModerateText(clientText, uploadedImage.SelectedComment, ResultsFile);
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://imgspotapi.azurewebsites.net/");
                var response = await client.PostAsJsonAsync("https://imgspotapi.azurewebsites.net/post", uploadedImage);

                if(response.IsSuccessStatusCode)
                {
                    TempData["Picture"] = uploadedImage.SelectedUser;
                    TempData["Comment"] = uploadedImage.SelectedComment;
                    return RedirectToPage("/user");
                }
            }
            return BadRequest();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}