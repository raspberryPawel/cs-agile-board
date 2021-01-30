using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AgileBoardView
{
    [Table("Positions")]
    [Index(nameof(positionId), IsUnique = true)]
    public partial class Position : IWithName
    {
        [Key]
        [Column("positionId")]
        public long positionId { get; set; }

        [Required]
        [Column(TypeName = "STRING")]
        public string Name { get; set; }

        public Position(string name) {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
