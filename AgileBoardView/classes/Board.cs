using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public static ListBox PrevSelectedList = null;

        public static ObservableCollection<TaskAndEmploy> OpenTasksList = new ObservableCollection<TaskAndEmploy>();
        public static ObservableCollection<TaskAndEmploy> CodingTasksList = new ObservableCollection<TaskAndEmploy>();
        public static ObservableCollection<TaskAndEmploy> TestsTasksList = new ObservableCollection<TaskAndEmploy>();
        public static ObservableCollection<TaskAndEmploy> ResolveTasksList = new ObservableCollection<TaskAndEmploy>();
        public static ObservableCollection<Employ> EmployeesList = new ObservableCollection<Employ>();

        public static ListBox CurrentlySelectedListRef = null;
        public static BoardColumns CurrentlySelectedColumn = BoardColumns.Any;
        public static TaskAndEmploy CurrentlySelectedTask = null;
        public static int CurrentlySelectedIndex = -1;

        public static Employ CurrentlySelectedEmploy = null;
       
        public static void AddNewTaskToBoard(Task task) {
            BoardDB.GetDB().Tasks.Add(task);
            if (BoardDB.GetDB().SaveChanges() == 1) {
                var employ = BoardDB.GetEmploy(task.employId);

                OpenTasksList.Add(new TaskAndEmploy(task, employ));
            }
        }

        public static void RestoreFromDB()
        {
            long openColumnId = BoardDB.GetColumnId(BoardColumns.Open);
            long codingColumnId = BoardDB.GetColumnId(BoardColumns.Coding);
            long testColumnId = BoardDB.GetColumnId(BoardColumns.Test);
            long resolveColumnId = BoardDB.GetColumnId(BoardColumns.Resolve);

            var openTasks = BoardDB.GetTasksAndEmployeesFromColumn(openColumnId);
            var codingTasks = BoardDB.GetTasksAndEmployeesFromColumn(codingColumnId);
            var testTasks = BoardDB.GetTasksAndEmployeesFromColumn(testColumnId);
            var resolveTasks = BoardDB.GetTasksAndEmployeesFromColumn(resolveColumnId);

            foreach (TaskAndEmploy t in openTasks) {
                OpenTasksList.Add(t);
            }

            foreach (TaskAndEmploy t in codingTasks)
                CodingTasksList.Add(t);

            foreach (TaskAndEmploy t in testTasks)
                TestsTasksList.Add(t);

            foreach (TaskAndEmploy t in resolveTasks)
                ResolveTasksList.Add(t);

        }

        public static void SetListContexts() {
            Board.OpenTasksRef.DataContext = Board.OpenTasksList;
            Board.CodingTasksRef.DataContext = Board.CodingTasksList;
            Board.TestsTasksRef.DataContext = Board.TestsTasksList;
            Board.ResolveTasksRef.DataContext = Board.ResolveTasksList;
        }

        public static void DeselectLists(bool open = true, bool coding = true, bool test = true, bool resolve = true) {
            if(open) OpenTasksRef.SelectedIndex = -1;
            if(coding) CodingTasksRef.SelectedIndex = -1;
            if(test) TestsTasksRef.SelectedIndex = -1;
            if(resolve) ResolveTasksRef.SelectedIndex = -1;
        }

        public static T GetKeyForValue<T>(string value, Dictionary<T, string> dictionary) {
            T key = dictionary.Keys.First();

            foreach (KeyValuePair<T, string> s in dictionary)
            {
                if (value == s.Value)
                    key = s.Key;
            }

            return key;
        }

        public static void DeselectPrevSelectedList() {
            if (!(Board.PrevSelectedList is null))
                Board.PrevSelectedList.SelectedIndex = -1;

            if (Board.OpenTasksRef.SelectedIndex >= 0) {
                Debug.WriteLine("ustawiam poprzednią na OpenTasksRef");
                PrevSelectedList = Board.OpenTasksRef;
            }
            if (Board.CodingTasksRef.SelectedIndex >= 0) {
                Debug.WriteLine("ustawiam poprzednią na CodingTasksRef");
                PrevSelectedList = Board.CodingTasksRef;
            }
            if (Board.TestsTasksRef.SelectedIndex >= 0) {
                Debug.WriteLine("ustawiam poprzednią na TestsTasksRef");
                PrevSelectedList = Board.TestsTasksRef;
            }
            if (Board.ResolveTasksRef.SelectedIndex >= 0) {
                Debug.WriteLine("ustawiam poprzednią na ResolveTasksRef");
                PrevSelectedList = Board.ResolveTasksRef;
            }
        }

        public static Tuple<ListBox, BoardColumns, TaskAndEmploy, int> GetSelectedListAndIndex() {
            int OpenTasksListIndex = Board.OpenTasksRef.SelectedIndex;
            int CodingTasksListIndex = Board.CodingTasksRef.SelectedIndex;
            int TestTasksListIndex = Board.TestsTasksRef.SelectedIndex;
            int ResolveTasksListIndex = Board.ResolveTasksRef.SelectedIndex;

            if (OpenTasksListIndex >= 0) {
                return new Tuple<ListBox, BoardColumns, TaskAndEmploy, int>(Board.OpenTasksRef, BoardColumns.Open, Board.OpenTasksList[OpenTasksListIndex], OpenTasksListIndex);
            }
            if (CodingTasksListIndex >= 0) { 
                return new Tuple<ListBox, BoardColumns, TaskAndEmploy, int>(Board.CodingTasksRef, BoardColumns.Coding, Board.CodingTasksList[CodingTasksListIndex], CodingTasksListIndex);
            }
            if (TestTasksListIndex >= 0) { 
                return new Tuple<ListBox, BoardColumns, TaskAndEmploy, int>(Board.TestsTasksRef, BoardColumns.Test, Board.TestsTasksList[TestTasksListIndex], TestTasksListIndex);
            }
            if (ResolveTasksListIndex >= 0) { 
                return new Tuple<ListBox, BoardColumns, TaskAndEmploy, int>(Board.ResolveTasksRef, BoardColumns.Resolve, Board.ResolveTasksList[ResolveTasksListIndex], ResolveTasksListIndex);
            }

            return null;
        }

        public static void SetAllNeededData(Tuple<ListBox, BoardColumns, TaskAndEmploy, int> data) {
            Board.CurrentlySelectedListRef = data.Item1;
            Board.CurrentlySelectedColumn = data.Item2;
            Board.CurrentlySelectedTask = data.Item3;
            Board.CurrentlySelectedIndex = data.Item4;
        }

        public static void MoveSelectedTaskToAnotherColumn(BoardColumns column) {
            TaskAndEmploy task = Board.GetListBasedOnSelectedColumn()[Board.CurrentlySelectedIndex];

            long newColumnId = BoardDB.GetColumnId(column);
            task.task.columnId = newColumnId;

            if (BoardDB.GetDB().SaveChanges() == 1) {
                Board.GetListBasedOnColumn(column).Add(task);
                Board.RemoveSelectedTask();
            }
        }

        public static void RemoveSelectedTask()
        {
            BoardDB.GetDB().Tasks.Remove(Board.CurrentlySelectedTask.task);
            Board.GetListBasedOnSelectedColumn().RemoveAt(Board.CurrentlySelectedIndex);
        }

        public static ObservableCollection<TaskAndEmploy> GetListBasedOnSelectedColumn() => Board.GetListBasedOnColumn(Board.CurrentlySelectedColumn);
        public static ObservableCollection<TaskAndEmploy> GetListBasedOnColumn(BoardColumns column) {
            switch(column) {
                case BoardColumns.Open: return Board.OpenTasksList;
                case BoardColumns.Coding: return Board.CodingTasksList;
                case BoardColumns.Test: return Board.TestsTasksList;
                case BoardColumns.Resolve: return Board.ResolveTasksList;
                default: return null;
            }
        }
    }
}
