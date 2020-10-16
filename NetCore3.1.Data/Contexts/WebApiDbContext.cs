using Microsoft.EntityFrameworkCore;
using NetCore3_1.Models.Entities;

namespace NetCore3_1.Data.Contexts
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
    }
}