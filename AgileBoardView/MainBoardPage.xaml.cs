using System;
using System.Windows;
using System.Windows.Controls;

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
            
            //Board.AddNewTaskToBoard(new Task());
            //Board.CodingTasksList.Add(new Task());
            //Board.TestsTasksList.Add(new Task());
            //Board.ResolveTasksList.Add(new Task());
            Board.SetListContexts();
        }

        private void OpenMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(open: false);
        private void CodingMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(coding: false);
        private void TestsMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(test: false);
        private void ResolveMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(resolve: false);

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            Board.DeselectLists();
            AddNewCardPage addNewCardPage = new AddNewCardPage();

            this.NavigationService.Navigate(addNewCardPage);
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            Tuple<ListBox, BoardColumns, Task, int> neededData = Board.GetSelectedListAndIndex();

            if (!(neededData is null))
            {
                Board.SetAllNeededData(neededData);

                EditTaskPage editTaskPage = new EditTaskPage();
                this.NavigationService.Navigate(editTaskPage);
            }
        }

        private void RemoveSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            Tuple<ListBox, BoardColumns, Task, int> neededData = Board.GetSelectedListAndIndex();

            if (!(neededData is null))
            {
                Board.SetAllNeededData(neededData);
                Board.RemoveSelectedTask();
            }
        }

        private void EmployButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesList employeesList = new EmployeesList();
            this.NavigationService.Navigate(employeesList);
        }
    }
}
