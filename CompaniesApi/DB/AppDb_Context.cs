using Microsoft.EntityFrameworkCore;

namespace CompaniesApi.DB
{
    public class AppDb_Context : DbContext
    {
        

        public AppDb_Context(DbContextOptions<AppDb_Context> options):base(options)
        {



        }

        public DbSet<Company> Company { get; set; }
        //public DbSet<Orders> Orders { get; set; }
        //public DbSet<Payment> Payment { get; set; }
        //public DbSet<Customer> Customer { get; set; }








    }
}
