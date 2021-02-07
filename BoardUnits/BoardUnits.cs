using AgileBoardView;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoardUnits
{

    [TestClass]
    public class BoardUnits
    {
        [TestMethod]
        public void GetEstimations()
        {
            Board.Estimates.Clear();
            Board.GetEstimations();

            Assert.IsTrue(Board.Estimates.Count > 0);
        }

        [TestMethod]
        public void GetColumns()
        {
            Board.Columns.Clear();
            Board.GetColumns();

            Assert.IsTrue(Board.Columns.Count > 0);
        }
        
        [TestMethod]
        public void GetPositions()
        {
            Board.PositionsList.Clear();
            Board.GetPositions();

            Assert.IsTrue(Board.PositionsList.Count > 0);
        }
        
        [TestMethod]
        public void GetEmployeesAndPositions()
        {
            Board.EmployAndPositionList.Clear();
            Board.GetEmployeesAndPositions();

            Assert.IsTrue(Board.EmployAndPositionList.Count > 0);
        }
        
        [TestMethod]
        public void RestoreTasksFromDB()
        {
            Board.RestoreFromDB();

            Assert.IsTrue(Board.OpenTasksList.Count > 0);
            Assert.IsTrue(Board.CodingTasksList.Count > 0);
            Assert.IsTrue(Board.TestsTasksList.Count > 0);
            Assert.IsTrue(Board.ResolveTasksList.Count > 0);
        }
        

        [TestMethod]
        public void GetKeyForValue()
        {
            Column value = Board.GetKeyForValue<Column>("Resolve", Board.Columns);
            Assert.IsTrue(value.Name == "Resolve");

            Estimate estimation = Board.GetKeyForValue<Estimate>("Low", Board.Estimates);
            Assert.IsTrue(estimation.Name == "Low");
        }
        

        [TestMethod]
        public void AddNewTaskToBoard()
        {
            Board.Estimates.Clear();
            Board.GetEstimations();

            Board.AddNewTaskToBoard(new Task("test name", "test description", Board.Estimates[1], DateTime.Now.AddDays(30), 1, 0));
            TaskAndEmploy t = Board.OpenTasksList[Board.OpenTasksList.Count -1];

            Assert.AreEqual(t.Name, "test name");
            Assert.AreEqual(t.Description, "test description");
            Assert.AreEqual(t.Estimation, "Medium");
            Assert.AreEqual(t.Assign, "Unassigned  ");
        }

        [TestMethod]
        public void RemoveSelectedTask()
        {
            Board.Columns.Clear();
            Board.Estimates.Clear();
            Board.PositionsList.Clear();
            Board.EmployAndPositionList.Clear();

            Board.GetColumns();
            Board.GetPositions();
            Board.GetEmployeesAndPositions();
            Board.GetEstimations();
            Board.RestoreFromDB();

            Board.CurrentlySelectedColumn = Board.GetKeyForValue<Column>("Open", Board.Columns);
            Board.CurrentlySelectedIndex = Board.OpenTasksList.Count - 1;

            int openTasksCount = Board.OpenTasksList.Count;
            Board.CurrentlySelectedTask = Board.OpenTasksList[Board.OpenTasksList.Count - 1];
            Board.RemoveSelectedTask();

            Assert.AreEqual(openTasksCount -1, Board.OpenTasksList.Count);
           
        }
    }
}
