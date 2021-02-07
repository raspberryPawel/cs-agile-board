using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AgileBoardView
{
    [Table("Columns")]
    [Index(nameof(columnId), IsUnique = true)]
    public partial class Column: IWithName
    {
        [Key]
        [Column("columnId")]
        public long columnId { get; set; }

        [Required]
        [Column(TypeName = "STRING")]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
