using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApp1.Models.Database
{
    [Table("POVOLANI")]
    public class Povolani
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
