using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AgileBoardView
{
    public class BoardDB
    {
        private static AgileBoardDB db = null;
        private static void CheckDBConnection()
        {
            if (BoardDB.db is null)
                BoardDB.db = new AgileBoardDB();
        }

        public static AgileBoardDB GetDB() {
            BoardDB.CheckDBConnection();

            return BoardDB.db;
        }

        public static IQueryable<TaskAndEmploy> GetTasksAndEmployeesFromColumn(long columnId)
        {
            var tasks = BoardDB.GetTasks();
            var employees = BoardDB.GetEmployees();
            
            return tasks
                .Where(t => t.columnId == columnId)
                .Join(employees, task => task.employId, employ => employ.employId,
                        (t, e) => new TaskAndEmploy(t, e));
        }

        public static int SaveChanges() => BoardDB.GetDB().SaveChanges();
        public static Employ GetEmploy(long employId) => BoardDB.GetEmployees().FirstOrDefault(p => p.employId == employId);
        public static DbSet<Employ> GetEmployees() => BoardDB.GetDB().Employees;
        public static DbSet<Task> GetTasks() => BoardDB.GetDB().Tasks;
        public static DbSet<Column> GetColumns() => BoardDB.GetDB().Columns;
        public static DbSet<Estimate> GetEstimations() => BoardDB.GetDB().Estimates;

    }
}
