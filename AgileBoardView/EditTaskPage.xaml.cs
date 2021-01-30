using System;
using System.Collections.Generic;
using System.Linq;
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

            foreach (var employ in BoardDB.GetEmployees())
                cbAssignee.Items.Add($"{employ.Name} {employ.Surname}");

            cbEstimation.Text = BoardConst.BoardEstimation[Board.CurrentlySelectedTask.Estimation];
            cbColumn.Text = BoardConst.BoardColumnsNames[Board.CurrentlySelectedColumn];

            Employ taskEmploy = BoardDB.GetEmployees().ToList().First(employ => employ.employId == Board.CurrentlySelectedTask.task.employId);
            cbAssignee.Text = $"{taskEmploy.Name} {taskEmploy.Surname}";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try {
                string name = txtName.Text;
                string description = txtDescription.Text;
                Employ assignee = BoardDB.GetEmployees().ToList().FirstOrDefault(employ => employ.ToString() == cbAssignee.Text);
                DateTime endDate = (DateTime)pickerDateEnd.SelectedDate;

                Board.CurrentlySelectedTask.task.Name = name;
                Board.CurrentlySelectedTask.task.Description = description;
                Board.CurrentlySelectedTask.task.TaskEndDate = endDate;
                Board.CurrentlySelectedTask.task.employId = assignee.employId;
                Board.CurrentlySelectedTask.task.Estimation = Board.GetKeyForValue<Estimate>(cbEstimation.Text, BoardConst.BoardEstimation);

                BoardColumns newColumn = Board.GetKeyForValue<BoardColumns>(cbColumn.Text, BoardConst.BoardColumnsNames);
                Board.CurrentlySelectedTask.employ = assignee;

                if (Board.CurrentlySelectedColumn != newColumn)
                    Board.MoveSelectedTaskToAnotherColumn(newColumn);

                if (BoardDB.GetDB().SaveChanges() == 1)
                    this.NavigationService.GoBack();

            }
            catch (InvalidOperationException err)
            {
                lblError.Content = err.Message;
            }
        }
    }
}
