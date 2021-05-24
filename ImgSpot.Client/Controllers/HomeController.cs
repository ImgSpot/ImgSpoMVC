using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImgSpot.Client.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace ImgSpot.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            ObjectModel subject = new ObjectModel();
            var client = new HttpClient();
            var response = client.GetAsync($"{_configuration["Services:webapi"]}/people/1/").GetAwaiter().GetResult();
            ObjectModel result = null;

            if(response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ObjectModel>();
                ViewBag.Object = result;
                return View("index");
            }
            return BadRequest();
            //return View("index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
