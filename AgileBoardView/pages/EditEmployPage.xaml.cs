using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

            txtName.Text = Board.CurrentlySelectedEmploy.employ.Name;
            txtSurname.Text = Board.CurrentlySelectedEmploy.employ.Surname;

            foreach (var p in Board.PositionsList)
                cbPosition.Items.Add(p);

            cbPosition.Text = Board.PositionsList.FirstOrDefault(p => p.positionId == Board.CurrentlySelectedEmploy.employ.positionId).ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string surname = txtSurname.Text;
                Position position = Board.PositionsList.First(p => p.ToString() == cbPosition.Text);

                Board.CurrentlySelectedEmploy.employ.Name = name;
                Board.CurrentlySelectedEmploy.employ.Surname = surname;
                Board.CurrentlySelectedEmploy.employ.positionId = position.positionId;
                Board.CurrentlySelectedEmploy.position = position;

                if (BoardDB.GetDB().SaveChanges() == 1)
                {
                    CollectionViewSource.GetDefaultView(Board.EmployAndPositionList).Refresh();
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
