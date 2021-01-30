using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgileBoardView
{

    public class TaskAndEmploy
    {
        public Task task;
        public Employ employ;

        public TaskAndEmploy(Task task, Employ employ)
        {
            this.task = task;
            this.employ = employ;
        }

        public string Name => task.Name;
        public string Description => task.Description;

        public Estimate Estimation => task.Estimation;
        public DateTime AddToBoardDate => task.AddToBoardDate;
        public DateTime LastModifyDate => task.LastModifyDate;
        public DateTime TaskEndDate => task.TaskEndDate;
        public string emp => employ.ToString();
    }

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

        public static long GetColumnId(BoardColumns column) => BoardDB.GetColumns().FirstOrDefault(p => p.Name == BoardConst.BoardColumnsNames[column]).columnId;
        public static Employ GetEmploy(long employId) => BoardDB.GetEmployees().FirstOrDefault(p => p.employId == employId);

        public static DbSet<Employ> GetEmployees() => BoardDB.GetDB().Employees;
        public static DbSet<Task> GetTasks() => BoardDB.GetDB().Tasks;
        public static DbSet<Column> GetColumns() => BoardDB.GetDB().Columns;

    }
}
