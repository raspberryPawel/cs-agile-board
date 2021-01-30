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
            if (Board.EmployeesList.Count == 0) {
                foreach (var employ in BoardDB.GetEmployees())
                {
                    if(employ.employId != 0)
                        Board.EmployeesList.Add(employ);
                }
            }
            
            InitializeComponent();

            Board.ListOfEmployeesRef = ListOfEmployees;
            Board.ListOfEmployeesRef.DataContext = Board.EmployeesList;
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
                Board.CurrentlySelectedEmploy = Board.EmployeesList[index];

                EditEmployPage EditEmployPage = new EditEmployPage();
                this.NavigationService.Navigate(EditEmployPage);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            int index = ListOfEmployees.SelectedIndex;
            if (index >= 0)
            {
                var employ = Board.EmployeesList[index];

                if (employ.employId != 0) {
                    BoardDB.GetEmployees().Remove(employ);

                    if (BoardDB.GetDB().SaveChanges() == 1)
                        Board.EmployeesList.RemoveAt(index);
                }
                else lblError.Content = "Nie można usunąć";
            }
        }
    }
}
