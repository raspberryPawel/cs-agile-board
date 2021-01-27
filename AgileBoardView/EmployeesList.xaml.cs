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
            foreach (var employ in BoardDB.GetEmployees())
                Board.EmployeesList.Add(employ);

            InitializeComponent();

            ListOfEmployees.DataContext = Board.EmployeesList;
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
    }
}
