using System.Windows;
using System.Windows.Controls;
using AgileBoardLogic;

namespace AgileBoardView
{
    public partial class MainBoardPage : Page
    {
        public MainBoardPage()
        {
            InitializeComponent();

            Board.OpenTasksRef = OpenTasks;
            Board.CodingTasksRef = CodingTasks;
            Board.TestsTasksRef = TestTasks;
            Board.ResolveTasksRef = ResolveTasks;
            
            Board.AddNewTaskToBoard(new Task());
            Board.CodingTasksList.Add(new Task());
            Board.TestsTasksList.Add(new Task());
            Board.ResolveTasksList.Add(new Task());
            Board.SetListContexts();
        }

        private void OpenMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(open: false);
        private void CodingMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(coding: false);
        private void TestsMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(test: false);
        private void ResolveMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(resolve: false);
        private void DeselectAll() => Board.DeselectLists();
    }
}
