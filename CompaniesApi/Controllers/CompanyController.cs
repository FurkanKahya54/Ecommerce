using CompaniesApi.DB;
using CompaniesApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace CompaniesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AppDb_Context _context;
        private readonly AppDb2_Context _context2;
        private readonly AppDb3_Context _context3;
        private readonly AppDb4_Context _context4;
        private readonly IHttpClientFactory _httpClientFactory;

        public CompanyController(AppDb_Context appDb_Context, IHttpClientFactory httpClientFactory, AppDb2_Context context2, AppDb3_Context context3, AppDb4_Context context4)
        {
            _context = appDb_Context;
            _httpClientFactory = httpClientFactory;
            _context2 = context2;
            _context3 = context3;
            _context4 = context4;
        }


        //[HttpGet("getall")]
        //public IActionResult GetAll()
        //{
        //    var sw = Stopwatch.StartNew();

        //    var companies = _context.Company.ToList();
        //    var orders = _context2.Orders.ToList();
        //    var customers = _context3.Customer.ToList();
        //    var payments = _context4.Payment.ToList();

        //    sw.Stop();

        //    var result = new ResultViewModel
        //    {
        //        CompanyViewModels = companies.Select(c => new CompanyViewModel
        //        {
        //            Id = c.Id,
        //            CompanyName = c.CompanyName,
        //            CompanyDescription = c.CompanyDescription,
        //            Locations = c.Locations
        //        }).ToList(),
        //        CustomerViewModels = customers.Select(c => new CustomerViewModel
        //        {
        //            Id = c.Id,
        //            CustomerName = c.CustomerName,
        //            CustomerDescription = c.CustomerDescription,
        //            Locations = c.Locations
        //        }).ToList(),
        //        OrderViewModels = orders.Select(o => new OrderViewModel
        //        {
        //            Id = o.Id,
        //            OrderName = o.OrderName,
        //            OrderDescription = o.OrderDescription,
        //            Locations = o.Locations
        //        }).ToList(),
        //        PaymentViewModels = payments.Select(p => new PaymentViewModel
        //        {
        //            Id = p.Id,
        //            CustomerName = p.PaymentName,
        //            CustomerDescription = p.PaymentDescription,
        //            Locations = p.Locations
        //        }).ToList(),
        //        ElapsedMilliseconds = sw.ElapsedMilliseconds
        //    };

        //    return Ok(result);
        //}
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var sw = Stopwatch.StartNew();

           
            var companies = _context.Company.ToList();
            var orders = _context2.Orders.ToList();
            var customers = _context3.Customer.ToList();
            var payments = _context4.Payment.ToList();

          
            foreach (var c in companies)
            {
                c.CompanyDescription = HeavyStringProcessing(c.CompanyDescription);
            }

            foreach (var c in customers)
            {
                c.CustomerDescription = HeavyStringProcessing(c.CustomerDescription);
            }

            foreach (var o in orders)
            {
                o.OrderDescription = HeavyStringProcessing(o.OrderDescription);
            }

            foreach (var p in payments)
            {
                p.PaymentDescription = HeavyStringProcessing(p.PaymentDescription);
            }

            sw.Stop();

        
            var result = new ResultViewModel
            {
                CompanyViewModels = companies.Select(c => new CompanyViewModel
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    CompanyDescription = c.CompanyDescription,
                    Locations = c.Locations
                }).ToList(),

                CustomerViewModels = customers.Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    CustomerName = c.CustomerName,
                    CustomerDescription = c.CustomerDescription,
                    Locations = c.Locations
                }).ToList(),

                OrderViewModels = orders.Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    OrderName = o.OrderName,
                    OrderDescription = o.OrderDescription,
                    Locations = o.Locations
                }).ToList(),

                PaymentViewModels = payments.Select(p => new PaymentViewModel
                {
                    Id = p.Id,
                    CustomerName = p.PaymentName,
                    CustomerDescription = p.PaymentDescription,
                    Locations = p.Locations
                }).ToList(),

                ElapsedMilliseconds = sw.ElapsedMilliseconds
            };

            return Ok(result);
        }

     

        //[HttpGet("parallel-tables")]
        //public async Task<IActionResult> GetParallelTables([FromServices] IDbContextFactory<AppDb_Context> contextFactory, IDbContextFactory<AppDb2_Context> contextFactory2, IDbContextFactory<AppDb3_Context> contextFactory3, IDbContextFactory<AppDb4_Context> contextFactory4)
        //{
        //    var sw = Stopwatch.StartNew();

        //    var taskCompanies = contextFactory.CreateDbContext().Company.ToListAsync();
        //    var taskOrders = contextFactory2.CreateDbContext().Orders.ToListAsync();
        //    var taskCustomers = contextFactory3.CreateDbContext().Customer.ToListAsync();
        //    var taskProducts = contextFactory4.CreateDbContext().Payment.ToListAsync();

        //    await Task.WhenAll(taskCompanies, taskCustomers, taskOrders, taskProducts);
        //    sw.Stop();
        //    var result = new ResultViewModel
        //    {
        //        CompanyViewModels = taskCompanies.Result.Select(c => new CompanyViewModel
        //        {
        //            Id = c.Id,
        //            CompanyName = c.CompanyName,
        //            CompanyDescription = c.CompanyDescription,
        //            Locations = c.Locations
        //        }).ToList(),

        //        CustomerViewModels = taskCustomers.Result.Select(c => new CustomerViewModel
        //        {
        //            Id = c.Id,
        //            CustomerName = c.CustomerName ,
        //            CustomerDescription = c.CustomerDescription,
        //            Locations = c.Locations
        //        }).ToList(),

        //        OrderViewModels = taskOrders.Result.Select(o => new OrderViewModel
        //        {
        //            Id = o.Id,
        //            OrderName = o.OrderName,
        //            OrderDescription = o.OrderDescription,
        //            Locations = o.Locations
        //        }).ToList(),

        //        PaymentViewModels = taskProducts.Result.Select(p => new PaymentViewModel
        //        {
        //            Id = p.Id,
        //            CustomerName = p.PaymentName,
        //            CustomerDescription = p.PaymentDescription,
        //            Locations = p.Locations
        //        }).ToList(),

        //        ElapsedMilliseconds = sw.ElapsedMilliseconds
        //    };











        //    return Ok(result);
        //}
        [HttpGet("parallel-tables")]
        public async Task<IActionResult> GetParallelTablesCpu([FromServices] IDbContextFactory<AppDb_Context> contextFactory,
                                                     IDbContextFactory<AppDb2_Context> contextFactory2,
                                                     IDbContextFactory<AppDb3_Context> contextFactory3,
                                                     IDbContextFactory<AppDb4_Context> contextFactory4)
        {
            var sw = Stopwatch.StartNew();

           
            var taskCompanies = contextFactory.CreateDbContext().Company.ToListAsync();
            var taskOrders = contextFactory2.CreateDbContext().Orders.ToListAsync();
            var taskCustomers = contextFactory3.CreateDbContext().Customer.ToListAsync();
            var taskPayments = contextFactory4.CreateDbContext().Payment.ToListAsync();

            await Task.WhenAll(taskCompanies, taskOrders, taskCustomers, taskPayments);

           
            var companies = taskCompanies.Result;
            var orders = taskOrders.Result;
            var customers = taskCustomers.Result;
            var payments = taskPayments.Result;

          
            Parallel.ForEach(companies, c =>
            {
               
                c.CompanyDescription = HeavyStringProcessing(c.CompanyDescription);
            });

            Parallel.ForEach(customers, c =>
            {
                c.CustomerDescription = HeavyStringProcessing(c.CustomerDescription);
            });

            Parallel.ForEach(orders, o =>
            {
                o.OrderDescription = HeavyStringProcessing(o.OrderDescription);
            });

            Parallel.ForEach(payments, p =>
            {
                p.PaymentDescription = HeavyStringProcessing(p.PaymentDescription);
            });

            sw.Stop();

            var result = new ResultViewModel
            {
                CompanyViewModels = companies.Select(c => new CompanyViewModel
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    CompanyDescription = c.CompanyDescription,
                    Locations = c.Locations
                }).ToList(),

                CustomerViewModels = customers.Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    CustomerName = c.CustomerName,
                    CustomerDescription = c.CustomerDescription,
                    Locations = c.Locations
                }).ToList(),

                OrderViewModels = orders.Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    OrderName = o.OrderName,
                    OrderDescription = o.OrderDescription,
                    Locations = o.Locations
                }).ToList(),

                PaymentViewModels = payments.Select(p => new PaymentViewModel
                {
                    Id = p.Id,
                    CustomerName = p.PaymentName,
                    CustomerDescription = p.PaymentDescription,
                    Locations = p.Locations
                }).ToList(),

                ElapsedMilliseconds = sw.ElapsedMilliseconds
            };

            return Ok(result);
        }

       
        private static string HeavyStringProcessing(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            for (int i = 0; i < 10000; i++)
            {
                input = new string(input.Reverse().ToArray());
            }
            return input;
        }





    }

}
