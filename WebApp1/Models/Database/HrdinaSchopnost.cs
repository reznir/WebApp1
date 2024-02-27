using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp1.Models.Database
{
    [PrimaryKey(nameof(HrdinaId), nameof(SchopnostId))]
    [Table("HRDINA_SCHOPNOST")]
    public class HrdinaSchopnost
    {
        [Column("HRDINA_ID"),ForeignKey(nameof(Hrdina))]
        public int HrdinaId { get; set; }
        public Hrdina? Hrdina { get; set; }


        [Column("SCHOPNOST_ID"), ForeignKey(nameof(Schopnost))]
        public int SchopnostId { get; set; }
        public Schopnost? Schopnost { get; set;}

        [Column("LEVEL")]
        public int Level { get; set; }
    }
}
