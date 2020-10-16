using Microsoft.EntityFrameworkCore;
using NetCore3_1.Models.Entities;

// NOTE
// To generate a new Migration you need to be positioned on this project
// dotnet ef migrations add [MIGRATION_NAME] -s ..\NetCore3.1.API\NetCore3.1.API.csproj

namespace NetCore3_1.Data.Contexts
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}