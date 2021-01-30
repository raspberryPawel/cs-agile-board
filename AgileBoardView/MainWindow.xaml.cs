using System.Windows.Navigation;

namespace AgileBoardView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            Board.GetColumns();
            Board.GetEstimations();
            Board.RestoreFromDB();

            InitializeComponent();
        }
    }
}
