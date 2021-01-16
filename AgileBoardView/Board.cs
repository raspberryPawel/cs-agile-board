using System.Collections.ObjectModel;
using System.Windows.Controls;
using AgileBoardLogic;

namespace AgileBoardView
{
    public class Board
    {
        public static ListBox OpenTasksRef = null;
        public static ListBox CodingTasksRef = null;
        public static ListBox TestsTasksRef = null;
        public static ListBox ResolveTasksRef = null;

        public static ObservableCollection<Task> OpenTasksList = new ObservableCollection<Task>();
        public static ObservableCollection<Task> CodingTasksList = new ObservableCollection<Task>();
        public static ObservableCollection<Task> TestsTasksList = new ObservableCollection<Task>();
        public static ObservableCollection<Task> ResolveTasksList = new ObservableCollection<Task>();

        public static BoardColumns CurrentlySelectedList = BoardColumns.Any;
        public static Task CurrentlySelectedTask = null;
        public static int CurrentlySelectedIndex = -1;


        public static void AddNewTaskToBoard(Task task) => OpenTasksList.Add(task);
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
    }
}
