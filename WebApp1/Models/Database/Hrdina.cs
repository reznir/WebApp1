using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp1.Models.Database;

namespace WebApp1.Models
{
    [Table("HRDINA")]
    public class Hrdina
    {
        public Hrdina()
        {
            Ohrozeni = 1;
            Vyhoda = 0;
            Name = "Name";
            TeloJizva = 0;
            TeloLimit = 5;
            Telo = TeloLimit;
            DuseJizva = 0;
            DuseLimit = 5;
            Duse = DuseLimit;
            VlivJizva = 0;
            VlivLimit = 5;
            Vliv = VlivLimit;
            Penize = 0;
            Suroviny = 0;
            Povolani = new List<Povolani>();
        }

        [Column("ID")]
        public int ID { get; set; }

        [Column("NAME",TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }

        [Column("OHROZENI")]
        public int Ohrozeni { get; set; }

        [Column("VYHODA")]
        public int Vyhoda { get; set; }

        #region Telo
        [Column("TELO_LIMIT")]
        public int TeloLimit { get; set; }

        [Column("TELO")]
        public int Telo { get; set; }

        [Column("TELO_JIZVA")]
        public int TeloJizva { get; set; }
        #endregion Telo

        #region Duse
        [Column("DUSE_LIMIT")]
        public int DuseLimit { get; set; }

        [Column("DUSE")]
        public int Duse { get; set; }

        [Column("DUSE_JIZVA")]
        public int DuseJizva { get; set; }
        #endregion Duse

        #region Vliv
        [Column("VLIV_LIMIT")]
        public int VlivLimit { get; set; }

        [Column("VLIV")]
        public int Vliv { get; set; }

        [Column("VLIV_JIZVA")]
        public int VlivJizva { get; set; }
        #endregion Vliv

        [Column("PENIZE")]
        public decimal Penize { set; get; }

        [Column("SUROVINY")]
        public int Suroviny { get; set; }

        [Column("VYBAVENI",TypeName = "NVARCHAR(MAX)")]
        public string? Vybaveni { get; set; }

        [Column("RASA", TypeName = "NVARCHAR(10)")]
        public string? Rasa { get; set; }
    }
}
