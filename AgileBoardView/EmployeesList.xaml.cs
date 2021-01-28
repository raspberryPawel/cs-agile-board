using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                    Board.EmployeesList.Add(employ);
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
            Board.CurrentlySelectedEmploy = Board.EmployeesList[index];

            EditEmployPage EditEmployPage = new EditEmployPage();
            this.NavigationService.Navigate(EditEmployPage);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            int index = ListOfEmployees.SelectedIndex;
            var employ = Board.EmployeesList[index];

            if (employ.employId != 0)
            {
                BoardDB.GetEmployees().Remove(employ);

                if (BoardDB.GetDB().SaveChanges() == 1)
                    Board.EmployeesList.RemoveAt(index);
            }
            else lblError.Content = "Nie można usunąć";
        }
    }
}
