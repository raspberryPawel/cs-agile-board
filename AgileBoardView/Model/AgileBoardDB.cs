using System;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable
namespace AgileBoardView
{
    public partial class AgileBoardDB : DbContext
    {
        public AgileBoardDB() { }
        public AgileBoardDB(DbContextOptions<AgileBoardDB> options) {}

        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Column> Columns { get; set; }
        public virtual DbSet<Employ> Employees { get; set; }
        public virtual DbSet<Estimate> Estimates { get; set; }
        public virtual DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\\model\AgileBoard.db"));

                optionsBuilder.UseSqlite($"Filename={newPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.AddToBoardDate).HasDefaultValueSql("datetime('%YYYY-%mm-%dd %HH:%MM:%SS')");

                entity.Property(e => e.LastModifyDate).HasDefaultValueSql("datetime('%YYYY-%mm-%dd %HH:%MM:%SS')");

                entity.Property(e => e.TaskEndDate).HasDefaultValueSql("datetime('%YYYY-%mm-%dd %HH:%MM:%SS', '+1 month')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
