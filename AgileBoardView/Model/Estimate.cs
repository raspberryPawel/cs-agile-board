using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace AgileBoardView
{
    [Table("Estimates")]
    [Index(nameof(estimateId), IsUnique = true)]
    public class Estimate
    {
        [Key]
        [Column("estimateId")]
        public long estimateId { get; set; }

        [Required]
        [Column(TypeName = "STRING")]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
