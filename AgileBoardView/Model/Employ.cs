using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AgileBoardView
{
    [Table("Employees")]
    [Index(nameof(employId), IsUnique = true)]
    public class Employ
    {
        [Key]
        [Column("employId")]
        public long employId { get; set; }

        [Required]
        [Column(TypeName = "STRING")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "STRING")]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "INT")]
        public long positionId { get; set; }

        public Employ(string name, string surname) : this(name, surname, 0) { }

        public Employ(string name, string surname, long position)
        {
            Name = name;
            Surname = surname;
            positionId = position;
        }

        public override string ToString() => $"{Name} {Surname}";
    }
}
