using Microsoft.EntityFrameworkCore;

namespace CompaniesApi.DB
{
    public class AppDb4_Context:DbContext
    {

        public AppDb4_Context(DbContextOptions<AppDb4_Context> options) : base(options)
        {

        }

        public DbSet<Payment> Payment { get; set; }




    }
}
