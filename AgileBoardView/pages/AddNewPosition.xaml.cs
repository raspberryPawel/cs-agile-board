using System;
using System.Windows;
using System.Windows.Controls;

namespace AgileBoardView
{
    /// <summary>
    /// Logika interakcji dla klasy AddNewEmploy.xaml
    /// </summary>
    public partial class AddNewPosition : Page
    {
        public AddNewPosition()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                var position = new Position(name);

                BoardDB.GetPositions().Add(position);
                if (BoardDB.GetDB().SaveChanges() == 1)
                {
                    Board.PositionsList.Add(position);
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
