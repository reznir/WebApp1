using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp1.Models.Database
{
    [PrimaryKey(nameof(HrdinaId), nameof(PovolaniId))]
    [Table("HRDINA_POVOLANI")]
    public class HrdinaPovolani
    {
        [Column("HRDINA_ID"), ForeignKey(nameof(Hrdina))]
        public int HrdinaId { get; set; }
        public Hrdina Hrdina { get; set; }


        [Column("POVOLANI_ID"), ForeignKey(nameof(Schopnost))]
        public int PovolaniId { get; set; }
        public Povolani Povolani { get; set; }

        [Column("LEVEL")]
        public int Level { get; set; }
    }
}
