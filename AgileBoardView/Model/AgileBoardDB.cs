using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace AgileBoardView
{
    public partial class AgileBoardDB : DbContext
    {
        public AgileBoardDB() { }
        public AgileBoardDB(DbContextOptions<AgileBoardDB> options) { }

        ///<summary>
        ///   returns Tasks column
        ///</summary>
        public virtual DbSet<Task> Tasks { get; set; }

        ///<summary>
        ///   returns Columns column
        ///</summary>
        public virtual DbSet<Column> Columns { get; set; }

        ///<summary>
        ///   returns Employees column
        ///</summary>
        public virtual DbSet<Employ> Employees { get; set; }

        ///<summary>
        ///   returns Estimates column
        ///</summary>
        public virtual DbSet<Estimate> Estimates { get; set; }

        ///<summary>
        ///   returns Positions column
        ///</summary>
        public virtual DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\\model\AgileBoard.db"));
                Debug.WriteLine("SIEMA SIEMA ===> ");
                Debug.WriteLine(newPath);
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
