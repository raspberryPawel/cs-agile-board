using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace AgileBoardView
{
    public class Board
    {
        /// <summary>
        /// OpenTasksRef keeps reference to Open Tasks ListBox
        /// </summary>
        public static ListBox OpenTasksRef = null;

        /// <summary>
        /// CodingTasksRef keeps reference to Coding Tasks ListBox
        /// </summary>
        public static ListBox CodingTasksRef = null;

        /// <summary>
        /// TestsTasksRef keeps reference to Tests Tasks ListBox
        /// </summary>
        public static ListBox TestsTasksRef = null;

        /// <summary>
        /// ResolveTasksRef keeps reference to Resolved Tasks ListBox
        /// </summary>
        public static ListBox ResolveTasksRef = null;

        /// <summary>
        /// ListOfEmployeesRef keeps reference to Employees ListBox
        /// </summary>
        public static ListBox ListOfEmployeesRef = null;

        /// <summary>
        /// ListOfPositionsRef keeps reference to Positions ListBox
        /// </summary>
        public static ListBox ListOfPositionsRef = null;

        /// <summary>
        /// EmployeesList keeps Employees
        /// </summary>
        public static ObservableCollection<Employ> EmployeesList = new ObservableCollection<Employ>();

        /// <summary>
        /// PositionsList keeps Positions
        /// </summary>
        public static ObservableCollection<Position> PositionsList = new ObservableCollection<Position>();

        /// <summary>
        /// EmployAndPositionList keeps Employees And Positions of the employ
        /// </summary>
        public static ObservableCollection<EmployAndPosition> EmployAndPositionList = new ObservableCollection<EmployAndPosition>();

        /// <summary>
        /// OpenTasksList keeps Open tasks
        /// </summary>
        public static ObservableCollection<TaskAndEmploy> OpenTasksList = new ObservableCollection<TaskAndEmploy>();

        /// <summary>
        /// CodingTasksList keeps coding tasks
        /// </summary>
        public static ObservableCollection<TaskAndEmploy> CodingTasksList = new ObservableCollection<TaskAndEmploy>();

        /// <summary>
        /// TestsTasksList keeps test tasks
        /// </summary>
        public static ObservableCollection<TaskAndEmploy> TestsTasksList = new ObservableCollection<TaskAndEmploy>();

        /// <summary>
        /// ResolveTasksList keeps resolve tasks
        /// </summary>
        public static ObservableCollection<TaskAndEmploy> ResolveTasksList = new ObservableCollection<TaskAndEmploy>();

        /// <summary>
        /// Board.Columns Dictionary keeps columnId as a key and column class as a value
        /// </summary>
        public static Dictionary<long, Column> Columns = new Dictionary<long, Column>();

        /// <summary>
        /// Board.Estimates Dictionary keeps estimateId as a key and Estimate class as a value
        /// </summary>
        public static Dictionary<long, Estimate> Estimates = new Dictionary<long, Estimate>();

        /// <summary>
        /// Board.CurrentlySelectedColumn keeps column from currently selected task list
        /// </summary>
        public static Column CurrentlySelectedColumn = null;

        /// <summary>
        /// Board.CurrentlySelectedEmploy keeps EmployAndPosition class from currently selected employ list
        /// </summary>
        public static EmployAndPosition CurrentlySelectedEmploy = null;

        /// <summary>
        /// Board.CurrentlySelectedPosition Position class frma currently selected position list
        /// </summary>
        public static Position CurrentlySelectedPosition = null;

        /// <summary>
        /// Board.CurrentlySelectedListRef keeps reference to currently selected column
        /// </summary>
        public static ListBox CurrentlySelectedListRef = null;

        /// <summary>
        /// Board.CurrentlySelectedTask keeps TaskAndEmploy class from currently selected column
        /// </summary>
        public static TaskAndEmploy CurrentlySelectedTask = null;

        /// <summary>
        /// Board.CurrentlySelectedIndex keeps currently selected index from tasks list
        /// </summary>
        public static int CurrentlySelectedIndex = -1;

        ///<summary>
        ///      Adding new task to open column
        ///      <example>
        ///        <code>
        ///          Task task = new Task(); <para/>
        ///          Board.AddNewTaskToBoard(task);
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void AddNewTaskToBoard(Task task)
        {
            BoardDB.GetTasks().Add(task);

            if (BoardDB.SaveChanges() == 1)
            {
                var employ = BoardDB.GetEmploy(task.employId);

                OpenTasksList.Add(new TaskAndEmploy(task, employ));
            }
        }


        ///<summary>
        ///      Get Estimates from DB and add them to Board.Estimates Dictionary
        ///      <example>
        ///        <code>
        ///          Board.GetEstimations();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void GetEstimations()
        {
            var estimations = BoardDB.GetEstimations();

            foreach (var est in estimations)
                Board.Estimates.Add(est.estimateId, est);
        }

        ///<summary>
        ///      Get Columns from DB and add them to Board.Columns Dictionary
        ///      <example>
        ///        <code>
        ///          Board.GetColumns();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void GetColumns()
        {
            var columns = BoardDB.GetColumns();

            foreach (var c in columns)
                Board.Columns.Add(c.columnId, c);
        }

        ///<summary>
        ///      Get Positions from DB and add them to Board.PositionsList Dictionary
        ///      <example>
        ///        <code>
        ///          Board.GetPositions();
        ///        </code>
        ///      </example>
        ///    </summary>

        public static void GetPositions()
        {
            var positions = BoardDB.GetPositions();

            foreach (var p in positions)
                Board.PositionsList.Add(p);
        }

        ///<summary>
        ///      Get EmployeesAndPositions from DB and add them to Board.GetEmployeesAndPositions Collection
        ///      <example>
        ///        <code>
        ///          Board.GetEmployeesAndPositions();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void GetEmployeesAndPositions()
        {
            var positions = BoardDB.GetEmployeesAndPosition();

            foreach (var p in positions)
                if (p.employ.employId != 0) Board.EmployAndPositionList.Add(p);
        }

        ///<summary>
        ///      Restore all tasks From DB and add them to relevant tasks Collection
        ///      <example>
        ///        <code>
        ///          Board.RestoreFromDB();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void RestoreFromDB()
        {
            long openColumnId = GetKeyForValue("Open", Columns).columnId;
            long codingColumnId = GetKeyForValue("Coding", Columns).columnId;
            long testColumnId = GetKeyForValue("Test", Columns).columnId;
            long resolveColumnId = GetKeyForValue("Resolve", Columns).columnId;

            var openTasks = BoardDB.GetTasksAndEmployeesFromColumn(openColumnId);
            var codingTasks = BoardDB.GetTasksAndEmployeesFromColumn(codingColumnId);
            var testTasks = BoardDB.GetTasksAndEmployeesFromColumn(testColumnId);
            var resolveTasks = BoardDB.GetTasksAndEmployeesFromColumn(resolveColumnId);

            foreach (TaskAndEmploy t in openTasks)
                OpenTasksList.Add(t);

            foreach (TaskAndEmploy t in codingTasks)
                CodingTasksList.Add(t);

            foreach (TaskAndEmploy t in testTasks)
                TestsTasksList.Add(t);

            foreach (TaskAndEmploy t in resolveTasks)
                ResolveTasksList.Add(t);
        }

        ///<summary>
        ///      Setting up DataContext to all lists references
        ///      <example>
        ///        <code>
        ///          Board.SetListContexts();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void SetListContexts()
        {
            Board.OpenTasksRef.DataContext = Board.OpenTasksList;
            Board.CodingTasksRef.DataContext = Board.CodingTasksList;
            Board.TestsTasksRef.DataContext = Board.TestsTasksList;
            Board.ResolveTasksRef.DataContext = Board.ResolveTasksList;
        }

        ///   <summary>
        ///      Deselect task from list.
        ///      <example>
        ///        <code>
        ///          Board.DeselectLists();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void DeselectLists(bool open = true, bool coding = true, bool test = true, bool resolve = true)
        {
            if (open) OpenTasksRef.SelectedIndex = -1;
            if (coding) CodingTasksRef.SelectedIndex = -1;
            if (test) TestsTasksRef.SelectedIndex = -1;
            if (resolve) ResolveTasksRef.SelectedIndex = -1;
        }

        ///   <summary>
        ///      Get Key For Value from dictionary
        ///      <example>
        ///        <code>
        ///         var columnId = Board.GetKeyForValue("Resolve", Columns).columnId
        ///        </code>
        ///      </example>
        ///    </summary>
        public static T GetKeyForValue<T>(string value, Dictionary<long, T> dictionary)
        {
            long key = dictionary.Keys.First();

            foreach (KeyValuePair<long, T> s in dictionary)
            {
                IWithName item = (IWithName)s.Value;
                if (value == item.Name)
                    key = s.Key;
            }

            return (T)dictionary[key];
        }


        ///   <summary>
        ///      Get selected list and index
        ///      <example>
        ///        <code>
        ///         Tuple<ListBox, Column, TaskAndEmploy, int> data = Board.GetSelectedListAndIndex();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static Tuple<ListBox, Column, TaskAndEmploy, int> GetSelectedListAndIndex()
        {
            int OpenTasksListIndex = Board.OpenTasksRef.SelectedIndex;
            int CodingTasksListIndex = Board.CodingTasksRef.SelectedIndex;
            int TestTasksListIndex = Board.TestsTasksRef.SelectedIndex;
            int ResolveTasksListIndex = Board.ResolveTasksRef.SelectedIndex;

            if (OpenTasksListIndex >= 0)
                return new Tuple<ListBox, Column, TaskAndEmploy, int>(Board.OpenTasksRef, GetKeyForValue("Open", Columns), Board.OpenTasksList[OpenTasksListIndex], OpenTasksListIndex);
            if (CodingTasksListIndex >= 0)
                return new Tuple<ListBox, Column, TaskAndEmploy, int>(Board.CodingTasksRef, GetKeyForValue("Coding", Columns), Board.CodingTasksList[CodingTasksListIndex], CodingTasksListIndex);
            if (TestTasksListIndex >= 0)
                return new Tuple<ListBox, Column, TaskAndEmploy, int>(Board.TestsTasksRef, GetKeyForValue("Test", Columns), Board.TestsTasksList[TestTasksListIndex], TestTasksListIndex);
            if (ResolveTasksListIndex >= 0)
                return new Tuple<ListBox, Column, TaskAndEmploy, int>(Board.ResolveTasksRef, GetKeyForValue("Resolve", Columns), Board.ResolveTasksList[ResolveTasksListIndex], ResolveTasksListIndex);

            return null;
        }

        ///   <summary>
        ///      Setting up all needed data to operate on tasks
        ///      <example>
        ///        <code>
        ///         Tuple<ListBox, Column, TaskAndEmploy, int> data = Board.GetSelectedListAndIndex(); <para/>
        ///         Board.SetAllNeededData(data);
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void SetAllNeededData(Tuple<ListBox, Column, TaskAndEmploy, int> data)
        {
            Board.CurrentlySelectedListRef = data.Item1;
            Board.CurrentlySelectedColumn = data.Item2;
            Board.CurrentlySelectedTask = data.Item3;
            Board.CurrentlySelectedIndex = data.Item4;
        }

        ///   <summary>
        ///      Move selected task to another column
        ///      <example>
        ///        <code>
        ///             Board.MoveSelectedTaskToAnotherColumn(newColumn);
        ///        </code>
        ///      </example>
        ///    </summary>
        public static int MoveSelectedTaskToAnotherColumn(Column column)
        {
            TaskAndEmploy task = Board.GetListBasedOnSelectedColumn()[Board.CurrentlySelectedIndex];

            task.task.columnId = column.columnId;
            int res = BoardDB.SaveChanges();

            if (res == 1)
            {
                Board.GetListBasedOnColumn(column).Add(task);
                Board.RemoveSelectedTask();
            }

            return res;
        }

        ///   <summary>
        ///      removes selected task
        ///      <example>
        ///        <code>
        ///             Board.RemoveSelectedTask();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static void RemoveSelectedTask()
        {
            BoardDB.GetTasks().Remove(Board.CurrentlySelectedTask.task);
            Board.GetListBasedOnSelectedColumn().RemoveAt(Board.CurrentlySelectedIndex);
        }

        ///   <summary>
        ///      returns Collection of TaskAndEmploy  based on currently selected column
        ///      <example>
        ///        <code>
        ///             ObservableCollection<TaskAndEmploy> list = Board.GetListBasedOnSelectedColumn();
        ///        </code>
        ///      </example>
        ///    </summary>
        public static ObservableCollection<TaskAndEmploy> GetListBasedOnSelectedColumn() => Board.GetListBasedOnColumn(Board.CurrentlySelectedColumn);

        ///   <summary>
        ///      returns Collection of TaskAndEmploy based on column
        ///      <example>
        ///        <code>
        ///             ObservableCollection<TaskAndEmploy> list = Board.GetListBasedOnColumn(column);
        ///        </code>
        ///      </example>
        ///    </summary>
        public static ObservableCollection<TaskAndEmploy> GetListBasedOnColumn(Column column)
        {
            switch (column.Name)
            {
                case "Open": return Board.OpenTasksList;
                case "Coding": return Board.CodingTasksList;
                case "Test": return Board.TestsTasksList;
                case "Resolve": return Board.ResolveTasksList;
                default: return null;
            }
        }
    }
}
