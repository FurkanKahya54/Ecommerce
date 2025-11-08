using Microsoft.EntityFrameworkCore;

namespace CompaniesApi.DB
{
    public class AppDb2_Context:DbContext
    {
        public AppDb2_Context(DbContextOptions<AppDb2_Context> options) : base(options)
        {
                
        }

        public DbSet<Orders> Orders { get; set; }









    }
}
