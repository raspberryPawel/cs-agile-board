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
                Estimate estimation = Board.GetKeyForValue<Estimate>(cbEstimation.Text, BoardConst.BoardEstimation);
                DateTime endDate = (DateTime)pickerDateEnd.SelectedDate;

                Task newTask = new Task(name, description, estimation, endDate);
                Board.AddNewTaskToBoard(newTask);

                this.NavigationService.GoBack();
            }
            catch (InvalidOperationException err){
                lblError.Content = err.Message;
            }
        }
    }
}
