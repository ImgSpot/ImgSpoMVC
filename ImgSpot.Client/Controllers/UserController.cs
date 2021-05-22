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

namespace ImgSpot.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var client = new HttpClient();
            var response = client.GetAsync($"{_logger["Services:webapi"]}").GetAwaiter().GetResult();
            List<string> result = null;

            if(response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<string>>(response.Content.ReadAsStringAsync().GetAwaiter);
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