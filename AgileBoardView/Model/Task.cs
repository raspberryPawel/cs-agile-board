using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AgileBoardView
{
    [Table("Tasks")]
    [Index(nameof(taskId), IsUnique = true)]
    public partial class Task
    {
        [Key]
        [Column("taskId")]
        public long taskId { get; set; }

        [Required]
        [Column(TypeName = "STRING")]
        public string Name { get; set; }
        
        [Column(TypeName = "STRING")]
        public string Description { get; set; }
        public long Estimation { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime AddToBoardDate { get; private set; }
        [Column(TypeName = "DATETIME")]
        public DateTime LastModifyDate { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime TaskEndDate { get; set; }

        [Column("columnId")]
        public long columnId { get; set; }

        [Column("employId")]
        public long employId { get; set; }

        public Task() : this("Brak", "Brak", Board.Estimates.First().Value, DateTime.Now.AddDays(30), 0, 0) { }
        public Task(string name, string description) : this(name, description, Board.Estimates.First().Value, DateTime.Now.AddDays(30), 0, 0) { }
        public Task(string name, string description, Estimate estimation) : this(name, description, estimation, DateTime.Now.AddDays(30), 0, 0) { }
        public Task(string name, string description, Estimate estimation, DateTime taskEndDate, long _columnId, long _employId)
        {
            Name = name;
            Description = description;
            Estimation = estimation.estimateId;
            TaskEndDate = taskEndDate;

            AddToBoardDate = DateTime.Now;
            LastModifyDate = DateTime.Now;

            columnId = _columnId;
            employId = _employId;

            this.ValidateDates(AddToBoardDate, TaskEndDate);
        }

        private void ValidateDates(DateTime first, DateTime second)
        {
            if (second < first) { throw new InvalidOperationException("End date must be grater than start date"); }
        }
        public override string ToString() => $"{Name}";
    }
}
