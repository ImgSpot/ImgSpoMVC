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

namespace ImgSpot.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

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
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(uploadedImage.Body);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "dfda2680db5a40fbb5a778cff852de54");
            var uri = "https://eastus.api.cognitive.microsoft.com/contentmoderator/moderate/v1.0/ProcessText/Screen?classify=True?" + queryString;
            HttpResponseMessage response;

            //Request Body
            byte[] byteData = Encoding.UTF8.GetBytes("body");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                response = await client.PostAsync(uri, content);
                return Ok(response);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}