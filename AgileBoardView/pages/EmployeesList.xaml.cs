using System.Windows;
using System.Windows.Controls;

namespace AgileBoardView
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeesList.xaml
    /// </summary>
    public partial class EmployeesList : Page
    {
        public EmployeesList()
        {
            Board.GetEmployeesAndPositions();

            InitializeComponent();

            Board.ListOfEmployeesRef = ListOfEmployees;
            Board.ListOfEmployeesRef.DataContext = Board.EmployAndPositionList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddNewEmploy AddEmploy = new AddNewEmploy();
            this.NavigationService.Navigate(AddEmploy);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainBoardPage boardPage = new MainBoardPage();
            this.NavigationService.Navigate(boardPage);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int index = ListOfEmployees.SelectedIndex;
            if (index >= 0) {
                Board.CurrentlySelectedEmploy = Board.EmployAndPositionList[index];

                EditEmployPage EditEmployPage = new EditEmployPage();
                this.NavigationService.Navigate(EditEmployPage);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            int index = ListOfEmployees.SelectedIndex;
            if (index >= 0)
            {
                var employ = Board.EmployAndPositionList[index].employ;

                if (employ.employId != 0) {
                    BoardDB.GetEmployees().Remove(employ);

                    if (BoardDB.GetDB().SaveChanges() == 1) {
                        Board.EmployAndPositionList.RemoveAt(index);
                        Board.ClearListsAndRestoreFromDB();
                    }
                }
                else lblError.Content = "Nie można usunąć";
            }
        }
    }
}
