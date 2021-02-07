using System;
using System.Linq;
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
            foreach (var p in Board.PositionsList)
                cbPosition.Items.Add(p);

            cbPosition.Text = Board.PositionsList.First().Name;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string surname = txtSurname.Text;
                Position position = Board.PositionsList.First(p => p.ToString() == cbPosition.Text);

                var employ = new Employ(name, surname, position.positionId);

                BoardDB.GetEmployees().Add(employ);
                if (BoardDB.GetDB().SaveChanges() == 1)
                {
                    Board.EmployAndPositionList.Add(new EmployAndPosition(employ, position));

                    this.NavigationService.GoBack();
                }
                else
                {
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
