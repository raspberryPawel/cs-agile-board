using System.Windows;
using System.Windows.Controls;

namespace AgileBoardView
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeesList.xaml
    /// </summary>
    public partial class PositionsList : Page
    {
        public PositionsList()
        {
            InitializeComponent();
            Board.ListOfPositionsRef = ListOfPositions;
            Board.ListOfPositionsRef.DataContext = Board.PositionsList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddNewPosition AddNewPosition = new AddNewPosition();
            Board.GetEmployeesAndPositions();
            this.NavigationService.Navigate(AddNewPosition);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainBoardPage boardPage = new MainBoardPage();
            this.NavigationService.Navigate(boardPage);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int index = ListOfPositions.SelectedIndex;
            if (index >= 0)
            {
                Board.CurrentlySelectedPosition = Board.PositionsList[index];
                Board.GetEmployeesAndPositions();
                EditPositionPage EditPositionPage = new EditPositionPage();
                this.NavigationService.Navigate(EditPositionPage);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            int index = ListOfPositions.SelectedIndex;
            if (index >= 0)
            {
                var position = Board.PositionsList[index];

                if (position.positionId != 0)
                {
                    BoardDB.GetPositions().Remove(position);

                    if (BoardDB.GetDB().SaveChanges() == 1)
                    {
                        Board.PositionsList.RemoveAt(index);
                    }
                }
                else lblError.Content = "Nie można usunąć";
            }
        }
    }
}
