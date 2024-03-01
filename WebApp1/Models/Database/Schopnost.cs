using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp1.Models.Database;

namespace WebApp1.Models
{
    [Table("SCHOPNOST")]
    public class Schopnost
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("NAME", TypeName = "NVARCHAR(20)")]
        public string Name { get; set; }

        [Column("DESCRIPTION",TypeName = "NVARCHAR(500)")]
        public string? Description { get; set; }

        [Column("PRAVIDLO", TypeName = "NVARCHAR(50)")]
        public string? Pravidlo { get; set;}

        [Column("VLASTNOST", TypeName = "NVARCHAR(5)")]
        public int? Vlastnost { get; set; }

        [Column("POVOLANI_ID"), ForeignKey("Povolani")]
        public int PovolaniId { get; set; }
        public Povolani? Povolani { get; set; }
    }
}
