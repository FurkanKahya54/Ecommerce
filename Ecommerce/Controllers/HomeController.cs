using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {


            return View();
        }


        public IActionResult NormalCrud()
        {

            var apiUrl = "https://localhost:7059/api/company";

            // Klasik HttpWebRequest kullanýmý (tam senkron)
            var request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";

            List<CompanyViewModel> companies = new List<CompanyViewModel>();

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                companies = JsonSerializer.Deserialize<List<CompanyViewModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return View(companies);

        }


        public async Task<IActionResult> ParalelCrud()
        {
            var apiUrl = "https://localhost:7059/api/company/parallel";

            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            ViewBag.ResultJson = json; 
            return View();
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
