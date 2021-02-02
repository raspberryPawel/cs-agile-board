using Microsoft.EntityFrameworkCore;
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

        ///   <summary>
        ///      returns DB
        ///      <example>
        ///        <code>
        ///             BoardDB.GetDB();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static AgileBoardDB GetDB() {
            BoardDB.CheckDBConnection();

            return BoardDB.db;
        }

        ///   <summary>
        ///      returns tasks with employees from BD
        ///      <example>
        ///        <code>
        ///             long openColumnId = GetKeyForValue("Open", Columns).columnId;<para/>
        ///             IQueryable<TaskAndEmploy> t = BoardDB.GetTasksAndEmployeesFromColumn(openColumnId);
        ///        </code>
        ///      </example>
        ///    </summary>
        public static IQueryable<TaskAndEmploy> GetTasksAndEmployeesFromColumn(long columnId)
        {
            var tasks = BoardDB.GetTasks();
            var employees = BoardDB.GetEmployees();
            
            return tasks
                .Where(t => t.columnId == columnId)
                .Join(employees, task => task.employId, employ => employ.employId,
                        (t, e) => new TaskAndEmploy(t, e));
        }

        ///   <summary>
        ///      returns employees with position from BD
        ///      <example>
        ///        <code>
        ///             IQueryable<EmployAndPosition> t = BoardDB.GetEmployeesAndPosition();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static IQueryable<EmployAndPosition> GetEmployeesAndPosition()
        {
            var positions = BoardDB.GetPositions();
            var employees = BoardDB.GetEmployees();

            return employees
                .Join(positions, employ => employ.positionId, position => position.positionId,
                        (e, p) => new EmployAndPosition(e, p));
                
        }

        ///   <summary>
        ///      saves changes in DB
        ///      <example>
        ///        <code>
        ///             BoardDB.SaveChanges();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static int SaveChanges() => BoardDB.GetDB().SaveChanges();

        ///   <summary>
        ///      returns employ with given id
        ///      <example>
        ///        <code>
        ///             BoardDB.GetEmploy(employId);
        ///        </code>
        ///      </example>
        ///    </summary>
        public static Employ GetEmploy(long employId) => BoardDB.GetEmployees().FirstOrDefault(p => p.employId == employId);

        ///   <summary>
        ///      returns Employees table from DB
        ///      <example>
        ///        <code>
        ///             BoardDB.GetEmployees();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static DbSet<Employ> GetEmployees() => BoardDB.GetDB().Employees;

        ///   <summary>
        ///      returns Tasks table from DB
        ///      <example>
        ///        <code>
        ///             BoardDB.GetTasks();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static DbSet<Task> GetTasks() => BoardDB.GetDB().Tasks;

        ///   <summary>
        ///      returns Columns table from DB
        ///      <example>
        ///        <code>
        ///             BoardDB.GetColumns();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static DbSet<Column> GetColumns() => BoardDB.GetDB().Columns;

        ///   <summary>
        ///      returns Estimates table from DB
        ///      <example>
        ///        <code>
        ///             BoardDB.GetEstimations();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static DbSet<Estimate> GetEstimations() => BoardDB.GetDB().Estimates;

        ///   <summary>
        ///      returns Positions table from DB
        ///      <example>
        ///        <code>
        ///             BoardDB.GetPositions();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static DbSet<Position> GetPositions() => BoardDB.GetDB().Positions;

    }
}
