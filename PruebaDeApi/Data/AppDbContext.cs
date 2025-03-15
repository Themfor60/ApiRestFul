using Microsoft.EntityFrameworkCore;
using PruebaDeApi.Models;

namespace PruebaDeApi.Data
{
    public class AppDbContext: DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> option ) : base( option ) 
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
