using AgileBoardLogic;
using System;
using System.Collections.Generic;
using System.Text;
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
    public partial class EditTaskPage : Page
    {
        public EditTaskPage()
        {
            InitializeComponent();

            txtName.Text = Board.CurrentlySelectedTask.Name;
            txtDescription.Text = Board.CurrentlySelectedTask.Description;
            pickerDateEnd.SelectedDate = Board.CurrentlySelectedTask.TaskEndDate;

            foreach (var s in BoardConst.BoardEstimation)
                cbEstimation.Items.Add(s.Value);

            foreach (var s in BoardConst.BoardColumnsNames)
                cbColumn.Items.Add(s.Value);

            cbEstimation.Text = BoardConst.BoardEstimation[Board.CurrentlySelectedTask.Estimation];
            cbColumn.Text = BoardConst.BoardColumnsNames[Board.CurrentlySelectedColumn];
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try {
                string name = txtName.Text;
                string description = txtDescription.Text;
                DateTime endDate = (DateTime)pickerDateEnd.SelectedDate;

                Board.CurrentlySelectedTask.Name = name;
                Board.CurrentlySelectedTask.Description = description;
                Board.CurrentlySelectedTask.TaskEndDate = endDate;
                Board.CurrentlySelectedTask.Estimation = Board.GetKeyForValue<Estimate>(cbEstimation.Text, BoardConst.BoardEstimation);

                BoardColumns newColumn = Board.GetKeyForValue<BoardColumns>(cbColumn.Text, BoardConst.BoardColumnsNames);

                if (Board.CurrentlySelectedColumn != newColumn)
                    Board.MoveSelectedTaskToAnotherColumn(newColumn);

                this.NavigationService.GoBack();
            }
            catch (InvalidOperationException err)
            {
                lblError.Content = err.Message;
            }
        }
    }
}
