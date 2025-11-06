using CompaniesApi.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;

namespace CompaniesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AppDb_Context _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public CompanyController(AppDb_Context appDb_Context, IHttpClientFactory httpClientFactory)
        {
            _context = appDb_Context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Company.ToList();
            return Ok(products);

        }
        [HttpGet("parallel")]
        public async Task<IActionResult> GetParallel([FromServices] IDbContextFactory<AppDb_Context> contextFactory)
        {
            var companyIds = await _context.Company
                .Select(c => c.Id)
                .ToListAsync();

            var results = new ConcurrentBag<Company>();

            await Parallel.ForEachAsync(companyIds, async (id, token) =>
            {
             
                await using var db = contextFactory.CreateDbContext();

                var company = await db.Company.FindAsync(new object[] { id }, token);
                if (company != null)
                {
                    results.Add(company);
                }
            });

            return Ok(results);
        }




    }

}
