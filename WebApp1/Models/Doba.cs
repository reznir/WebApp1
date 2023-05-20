using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp1.Models
{
    [Table("DOBA")]
    public class Doba
    {
        public int Id { get; set; }
        [Column("DOBA_POPIS",TypeName ="nvarchar(50)")]
        public string DobaDef { get; set; }
        [Column("CELY_NAZEV",TypeName ="nvarchar(50)")]
        public string Cely_nazev { get; set; }
    }
}
