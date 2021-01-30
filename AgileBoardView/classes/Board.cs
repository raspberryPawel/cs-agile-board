using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace AgileBoardView
{
    public class Board
    {
        public static ListBox OpenTasksRef = null;
        public static ListBox CodingTasksRef = null;
        public static ListBox TestsTasksRef = null;
        public static ListBox ResolveTasksRef = null;
        public static ListBox ListOfEmployeesRef = null;

        public static ObservableCollection<Employ> EmployeesList = new ObservableCollection<Employ>();
        public static ObservableCollection<TaskAndEmploy> OpenTasksList = new ObservableCollection<TaskAndEmploy>();
        public static ObservableCollection<TaskAndEmploy> CodingTasksList = new ObservableCollection<TaskAndEmploy>();
        public static ObservableCollection<TaskAndEmploy> TestsTasksList = new ObservableCollection<TaskAndEmploy>();
        public static ObservableCollection<TaskAndEmploy> ResolveTasksList = new ObservableCollection<TaskAndEmploy>();

        public static Dictionary<long, Column> Columns = new Dictionary<long, Column>();
        public static Dictionary<long, Estimate> Estimates = new Dictionary<long, Estimate>();

        public static Column CurrentlySelectedColumn = null;
        public static Employ CurrentlySelectedEmploy = null;
        public static ListBox CurrentlySelectedListRef = null;
        public static TaskAndEmploy CurrentlySelectedTask = null;

        public static int CurrentlySelectedIndex = -1;

        public static void AddNewTaskToBoard(Task task)
        {
            BoardDB.GetTasks().Add(task);

            if (BoardDB.SaveChanges() == 1)
            {
                var employ = BoardDB.GetEmploy(task.employId);

                OpenTasksList.Add(new TaskAndEmploy(task, employ));
            }
        }

        public static void GetEstimations()
        {
            var estimations = BoardDB.GetEstimations();

            foreach (var est in estimations)
                Board.Estimates.Add(est.estimateId, est);
        }
        public static void GetColumns()
        {
            var columns = BoardDB.GetColumns();

            foreach (var c in columns)
                Board.Columns.Add(c.columnId, c);
        }

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

        public static void SetListContexts()
        {
            Board.OpenTasksRef.DataContext = Board.OpenTasksList;
            Board.CodingTasksRef.DataContext = Board.CodingTasksList;
            Board.TestsTasksRef.DataContext = Board.TestsTasksList;
            Board.ResolveTasksRef.DataContext = Board.ResolveTasksList;
        }

        public static void DeselectLists(bool open = true, bool coding = true, bool test = true, bool resolve = true)
        {
            if (open) OpenTasksRef.SelectedIndex = -1;
            if (coding) CodingTasksRef.SelectedIndex = -1;
            if (test) TestsTasksRef.SelectedIndex = -1;
            if (resolve) ResolveTasksRef.SelectedIndex = -1;
        }

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

        public static void SetAllNeededData(Tuple<ListBox, Column, TaskAndEmploy, int> data)
        {
            Board.CurrentlySelectedListRef = data.Item1;
            Board.CurrentlySelectedColumn = data.Item2;
            Board.CurrentlySelectedTask = data.Item3;
            Board.CurrentlySelectedIndex = data.Item4;
        }

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

        public static void RemoveSelectedTask()
        {
            BoardDB.GetTasks().Remove(Board.CurrentlySelectedTask.task);
            Board.GetListBasedOnSelectedColumn().RemoveAt(Board.CurrentlySelectedIndex);
        }

        public static ObservableCollection<TaskAndEmploy> GetListBasedOnSelectedColumn() => Board.GetListBasedOnColumn(Board.CurrentlySelectedColumn);
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
