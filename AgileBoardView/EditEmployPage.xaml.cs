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
    /// Logika interakcji dla klasy EditEmployPage.xaml
    /// </summary>
    public partial class EditEmployPage : Page
    {
        public EditEmployPage()
        {
            InitializeComponent();

            txtName.Text = Board.CurrentlySelectedEmploy.Name;
            txtSurname.Text = Board.CurrentlySelectedEmploy.Surname;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string surname = txtSurname.Text;

                Board.CurrentlySelectedEmploy.Name = name;
                Board.CurrentlySelectedEmploy.Surname = surname;

                if (BoardDB.GetDB().SaveChanges() == 1)
                {
                    CollectionViewSource.GetDefaultView(Board.EmployeesList).Refresh();
                    this.NavigationService.GoBack();
                }
                else lblError.Content = "Błąd edycji";
            }
            catch (InvalidOperationException err)
            {
                lblError.Content = err.Message;
            }
        }
    }
}
