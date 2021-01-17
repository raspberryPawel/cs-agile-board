using AgileBoardLogic;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AgileBoardView
{
    public partial class AddNewCardPage : Page
    {
        public AddNewCardPage()
        {
            InitializeComponent();
            foreach (var s in BoardConst.BoardEstimation)
                cbEstimation.Items.Add(s.Value);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e) => this.NavigationService.GoBack();

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string description = txtDescription.Text;
                string estimation = cbEstimation.Text;
                DateTime endDate = (DateTime)pickerDateEnd.SelectedDate;
                Estimate est = Estimate.Low;

                foreach (KeyValuePair<Estimate, string> s in BoardConst.BoardEstimation)
                {
                    if (estimation == s.Value)
                        est = s.Key;
                }

                Task newTask = new Task(name, description, est, endDate);
                Board.AddNewTaskToBoard(newTask);

                this.NavigationService.GoBack();
            }
            catch (InvalidOperationException err){
                lblError.Content = err.Message;
            }
        }
    }
}
