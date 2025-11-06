using Microsoft.EntityFrameworkCore;

namespace CompaniesApi.DB
{
    public class AppDb_Context : DbContext
    {
        

        public AppDb_Context(DbContextOptions<AppDb_Context> options):base(options)
        {



        }

        public DbSet<Company> Company { get; set; }








    }
}
