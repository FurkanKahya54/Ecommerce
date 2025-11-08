using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

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


        //public IActionResult NormalCrud()
        //{

        //    var apiUrl = "https://localhost:7059/api/company";

        //    // Klasik HttpWebRequest kullanýmý (tam senkron)
        //    var request = (HttpWebRequest)WebRequest.Create(apiUrl);
        //    request.Method = "GET";

        //    List<CompanyViewModel> companies = new List<CompanyViewModel>();

        //    using (var response = (HttpWebResponse)request.GetResponse())
        //    using (var stream = response.GetResponseStream())
        //    using (var reader = new StreamReader(stream))
        //    {
        //        var json = reader.ReadToEnd();
        //        companies = JsonSerializer.Deserialize<List<ResultViewModel>>(json, new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });
        //    }

        //    return View(companies);

        //}

        //public IActionResult NormalCrud()
        //{
        //    var apiUrl = "https://localhost:7059/api/company";

        //    // Klasik HttpWebRequest kullanýmý (tam senkron)
        //    var request = (HttpWebRequest)WebRequest.Create(apiUrl);
        //    request.Method = "GET";

        //    ResultViewModel result = null;

        //    using (var response = (HttpWebResponse)request.GetResponse())
        //    using (var stream = response.GetResponseStream())
        //    using (var reader = new StreamReader(stream))
        //    {
        //        var json = reader.ReadToEnd();
        //        result = JsonSerializer.Deserialize<ResultViewModel>(json, new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });
        //    }

        //    return View(result);
        //}
        //public IActionResult NormalCrud()
        //{
        //    var apiUrl = "https://localhost:7059/api/company/getall";

        //    var request = (HttpWebRequest)WebRequest.Create(apiUrl);
        //    request.Method = "GET";

        //    ResultViewModel result;
        //    long elapsedMs;

        //    using (var response = (HttpWebResponse)request.GetResponse())
        //    using (var stream = response.GetResponseStream())
        //    using (var reader = new StreamReader(stream))
        //    {
        //        var json = reader.ReadToEnd();

        //        var obj = JsonSerializer.Deserialize<JsonElement>(json);
        //        elapsedMs = obj.GetProperty("ElapsedMilliseconds").GetInt64();

        //        result = JsonSerializer.Deserialize<ResultViewModel>(json, new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });
        //    }

        //    ViewBag.Elapsed = elapsedMs;
        //    return View(result);
        //}
        //public async Task<IActionResult> ParalelCrud()
        //{
        //    var apiUrl = "https://localhost:7059/api/company/parallel-tables";

        //    var response = await _httpClient.GetAsync(apiUrl);
        //    response.EnsureSuccessStatusCode();

        //    var json = await response.Content.ReadAsStringAsync();

        //    ViewBag.ResultJson = json; 
        //    return View();
        //}
        public IActionResult NormalCrud()
        {
            var apiUrl = "https://localhost:7059/api/company/getall";

            var request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";

            ResultViewModel result;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();

                // JsonElement ile uðraþma, direkt ResultViewModel'e deserialize et
                result = JsonSerializer.Deserialize<ResultViewModel>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            // ElapsedMilliseconds artýk direkt ResultViewModel üzerinden alýnabilir
            ViewBag.Elapsed = result.ElapsedMilliseconds;

            return View(result);
        }
        public async Task<IActionResult> ParallelCrud()
        {
            var apiUrl = "https://localhost:7059/api/company/parallel-tables";

          
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

           
            var result = JsonSerializer.Deserialize<ResultViewModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

          
            ViewBag.Elapsed = result.ElapsedMilliseconds;

            return View(result);
        }

        //public async  Task<IActionResult> ParallelCrud()
        //{
        //    var apiUrl = "https://localhost:7059/api/company/parallel";

        //    var request = (HttpWebRequest)WebRequest.Create(apiUrl);
        //    request.Method = "GET";

        //    ResultViewModel result;
        //    long elapsedMs;

        //    using (var response = (HttpWebResponse)request.GetResponse())
        //    using (var stream = response.GetResponseStream())
        //    using (var reader = new StreamReader(stream))
        //    {
        //        var json = reader.ReadToEnd();

        //        var obj = JsonSerializer.Deserialize<JsonElement>(json);
        //        elapsedMs = obj.GetProperty("ElapsedMilliseconds").GetInt64();

        //        result = JsonSerializer.Deserialize<ResultViewModel>(json, new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        });
        //    }

        //    ViewBag.Elapsed = elapsedMs;
        //    return View(result);
        //}

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
