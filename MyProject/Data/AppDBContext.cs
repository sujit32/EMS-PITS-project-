using Microsoft.EntityFrameworkCore;
using MyProject.Models;
namespace MyProject.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Models.Address> Addresses { get; set; }
    }
}
