using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicShopWebAPI;
using MusicShopWebApp.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace MusicShopWebApp.Controllers
{
    public class MusicShopController : Controller
    {
        private readonly ILogger<MusicShopController> _logger;
        private readonly HttpClient httpClient;
        private const string apiBaseUrl = "https://localhost:44374/api/musicshop";

        public MusicShopController(ILogger<MusicShopController> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();
        }

        public IActionResult Instruments(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            IEnumerable<MusicInstrument> instruments = null;

            HttpResponseMessage response = httpClient.GetAsync(apiBaseUrl + "/instruments").Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content.ReadAsStringAsync().Result;
                instruments = JsonSerializer.Deserialize<IEnumerable<MusicInstrument>>(jsonString);
            }

            return View(instruments);
        }

        public IActionResult Details(MusicInstrument inst)
        {
            return View(inst);
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