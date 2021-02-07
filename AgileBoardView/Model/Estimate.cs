using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace AgileBoardView
{
    [Table("Estimates")]
    [Index(nameof(estimateId), IsUnique = true)]
    public class Estimate : IWithName
    {
        public Estimate() { }
        public Estimate(long estimateId, string name)
        {
            this.estimateId = estimateId;
            this.Name = name;
        }

        [Key]
        [Column("estimateId")]
        public long estimateId { get; set; }

        [Required]
        [Column(TypeName = "STRING")]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
