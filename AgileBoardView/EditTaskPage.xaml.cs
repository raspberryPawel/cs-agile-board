using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

            foreach (var s in Board.Estimates)
                cbEstimation.Items.Add(s.Value);

            foreach (var s in Board.Columns)
                cbColumn.Items.Add(s.Value);

            foreach (var employ in BoardDB.GetEmployees())
                cbAssignee.Items.Add(employ);

            cbEstimation.Text = Board.CurrentlySelectedTask.Estimation;
            cbColumn.Text = Board.Columns[Board.CurrentlySelectedTask.task.columnId].ToString();

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
                Board.CurrentlySelectedTask.task.Estimation = Board.GetKeyForValue(cbEstimation.Text, Board.Estimates).estimateId;

                Column newColumn = Board.GetKeyForValue(cbColumn.Text, Board.Columns);
                Board.CurrentlySelectedTask.employ = assignee;

                int result = -1;
                if (Board.CurrentlySelectedColumn != newColumn)
                    result = Board.MoveSelectedTaskToAnotherColumn(newColumn);


                if (result < 0 && BoardDB.GetDB().SaveChanges() == 1) this.NavigationService.GoBack();
                else this.NavigationService.GoBack();
            }
            catch (InvalidOperationException err)
            {
                lblError.Content = err.Message;
            }
        }
    }
}
