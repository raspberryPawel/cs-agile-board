using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

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
        public Estimate Estimation { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime AddToBoardDate { get; private set; }
        [Column(TypeName = "DATETIME")]
        public DateTime LastModifyDate { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime TaskEndDate { get; set; }

        [Column("columnId")]
        public long columnId { get; set; }

        #region constructors
        public Task() : this("Brak", "Brak", Estimate.Low, DateTime.Now.AddDays(30)) { }
        public Task(string name, string description) : this(name, description, Estimate.Low, DateTime.Now.AddDays(30)) { }
        public Task(string name, string description, Estimate estimation) : this(name, description, estimation, DateTime.Now.AddDays(30)) { }
        public Task(string name, string description, Estimate estimation, DateTime taskEndDate)
        {
            Name = name;
            Description = description;
            Estimation = estimation;
            TaskEndDate = taskEndDate;

            AddToBoardDate = DateTime.Now;
            LastModifyDate = DateTime.Now;
            columnId = BoardDB.GetColumnId(BoardColumns.Open);

            this.ValidateDates(AddToBoardDate, TaskEndDate);
        }
        #endregion
        private void ValidateDates(DateTime first, DateTime second)
        {
            if (second < first) { throw new InvalidOperationException("End date must be grater than start date"); }
        }
        public override string ToString() => $"{Name}";
    }
}
