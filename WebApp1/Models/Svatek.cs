using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp1.Models
{
    [Table("SVATEK")]
    public class Svatek
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("NAZEV_DNE",TypeName ="NVARCHAR(50)")]
        public string Nazev_dne { get; set; }
        [Column("CELY_NAZEV",TypeName ="NVARCHAR(50)")]
        public string Cely_nazev { get; set; }
        [Column("CTENI",TypeName ="NVARCHAR(MAX)")]
        public string? Cteni { get; set; }
        [Column("BARVA",TypeName ="decimal(10:0)")]
        public decimal? Barva { get; set; }
        [Column("CYKLY")]
        public bool? Cykly { get;set; }
        [Column("DOBA_ID"),ForeignKey("DOBA")]        
        public int? DobaId { get; set; }
        public virtual Doba NavDoba { get; set; }
    }
}
