using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Ejercicio1.Models
{
    public class biblioContext : DbContext
    {
        public biblioContext(DbContextOptions<biblioContext> options) : base(options)
        {
        }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Libro> libro { get; set; }


    }
}
