using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
