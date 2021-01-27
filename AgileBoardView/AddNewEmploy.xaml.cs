using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy AddNewEmploy.xaml
    /// </summary>
    public partial class AddNewEmploy : Page
    {
        public AddNewEmploy()
        {
            InitializeComponent();
            foreach (var s in BoardConst.BoardEstimation)
                cbPosition.Items.Add(s.Value);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string surname = txtSurname.Text;

                var employ = new Employ(name, surname);

                BoardDB.GetEmployees().Add(employ);
                if (BoardDB.GetDB().SaveChanges() == 1)
                {
                    Board.EmployeesList.Add(employ);
                    this.NavigationService.GoBack();
                }
                else {
                    lblError.Content = "Błąd dodawania";
                }
            }
            catch (InvalidOperationException err)
            {
                lblError.Content = err.Message;
            }
        }
    }
}
