using Microsoft.EntityFrameworkCore;

namespace CompaniesApi.DB
{
    public class AppDb3_Context:DbContext
    {
        public AppDb3_Context(DbContextOptions<AppDb3_Context> options) : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }



    }
}
