using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp1.Models;
using WebApp1.Models.Database;

namespace WebApp1.DataAccess
{
    public class LitTextyDbContext : DbContext
    {
        public LitTextyDbContext(DbContextOptions options):base(options)
        { }
        public DbSet<LitText> LitText { get; set; }
        public DbSet<Svatek> Svatek { get; set; }
        public DbSet<Doba> Doba { get; set; }

        public DbSet<Hrdina> Hrdina { get; set; }
        public DbSet<Schopnost> Schopnost { get; set; }
        public DbSet<Povolani> Povolani { get; set; }
        public DbSet<HrdinaSchopnost> HrdinaSchopnosts { get; set; }
        public DbSet<Models.Database.HrdinaPovolani> HrdinaPovolani { get;set; }
    }
}
