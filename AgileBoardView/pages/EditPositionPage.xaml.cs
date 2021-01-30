using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AgileBoardView
{
    /// <summary>
    /// Logika interakcji dla klasy EditEmployPage.xaml
    /// </summary>
    public partial class EditPositionPage : Page
    {
        public EditPositionPage()
        {
            InitializeComponent();

            txtName.Text = Board.CurrentlySelectedPosition.Name;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;

                Board.CurrentlySelectedPosition.Name = name;

                if (BoardDB.GetDB().SaveChanges() == 1)
                {
                    CollectionViewSource.GetDefaultView(Board.PositionsList).Refresh();
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
