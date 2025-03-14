﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp1.Models
{
    [Table("LIT_TEXT")]
    public class LitText
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAZEV_DNE", TypeName = "NVARCHAR(50)")]
        public string Nazev_dne { get; set; }

        [Column("CYKLUS", TypeName = "NVARCHAR(1)")]
        public string? Cyklus { get; set; }

        [Column("TEXT", TypeName = "NVARCHAR(MAX)")]
        public string? Text { get; set; }

        /// <summary>
        /// Text without html tags
        /// </summary>
        [Column("PLAIN_TEXT", TypeName = "NVARCHAR(MAX)")]
        public string? PlainText { get; set; }

        [Column("Created")]
        public DateTime Created { get; set; }

        [Column("Updated")]
        public DateTime? Updated { get; set; }

        [Column("SVATEK_ID"), ForeignKey("Svatek")]
        public int SvatekId { get; set; }
        public Svatek? Svatek { get; set; }

    }
}
