using System.Windows;

namespace AgileBoardView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OpenMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(open: false);
        private void CodingMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(coding: false);
        private void TestsMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(test: false);
        private void ResolveMouseDown(object sender, RoutedEventArgs e) => Board.DeselectLists(resolve: false);
    }
}
