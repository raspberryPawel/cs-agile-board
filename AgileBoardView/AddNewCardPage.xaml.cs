using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AgileBoardView
{
    public partial class AddNewCardPage : Page
    {
        public AddNewCardPage()
        {
            InitializeComponent();
            foreach (var s in Board.Estimates)
                cbEstimation.Items.Add(s.Value);

            foreach (var employ in BoardDB.GetEmployees())
            {
                cbAssignee.Items.Add($"{employ.Name} {employ.Surname}");
                if (employ.employId == 0)
                    cbAssignee.Text = $"{employ.Name} {employ.Surname}";
            }

            cbEstimation.Text = Board.Estimates.First().Value.Name;
            pickerDateEnd.SelectedDate = DateTime.Now.AddDays(7);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string description = txtDescription.Text;
                Estimate estimation = Board.GetEstimationForName(cbEstimation.Text);
                DateTime endDate = (DateTime)pickerDateEnd.SelectedDate;
                long employId = BoardDB.GetEmployees().ToList().First(employ => $"{employ.Name} {employ.Surname}" == cbAssignee.Text).employId;
                long openColumnId = Board.GetColumnForName("Open").columnId;

                Task newTask = new Task(name, description, estimation, endDate, openColumnId, employId);
                Board.AddNewTaskToBoard(newTask);

                this.NavigationService.GoBack();
            }
            catch (InvalidOperationException err){
                lblError.Content = $"{err.Message} {cbAssignee.Text}";
            }
        }
    }
}
