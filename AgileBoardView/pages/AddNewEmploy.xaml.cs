using System;
using System.Windows;
using System.Windows.Controls;

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
            foreach (var employ in BoardDB.GetEmployees())
                cbPosition.Items.Add(employ);

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
