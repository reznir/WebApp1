using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp1.Models;

namespace WebApp1.DataAccess
{
    public class LitTextyDbContext : DbContext
    {
        public LitTextyDbContext(DbContextOptions options):base(options)
        { }
        public DbSet<LitText> LitText { get; set; }
        public DbSet<Svatek> Svatek { get; set; }
        public DbSet<Doba> Doba { get; set; }
    }
}
