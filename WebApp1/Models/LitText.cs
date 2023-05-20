using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp1.Models
{
    [Table("LIT_TEXT")]
    public class LitText
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("NAZEV_DNE",TypeName ="NVARCHAR(50)")]
        public string Nazev_dne { get; set; }
        [Column("CYKLUS",TypeName ="NVARCHAR(1)")]
        public string Cyklus { get; set; }
        [Column("TEXT",TypeName ="NVARCHAR(MAX)")]
        public string Text { get; set; }
        [Column("SVATEK_ID")]
        public int SvateId { get; set; }
        public virtual Svatek NavSvatek { get; set; }

    }
}
