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
            var subject = new ObjectModel();
            var client = new HttpClient();
            var response = client.GetAsync($"{_configuration["Services:webapi"]}/people/1").GetAwaiter().GetResult();
            ObjectModel result = null;

            if(response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<ObjectModel>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                ViewBag.Object = result;
                return View("index");
            }
            return null;
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}