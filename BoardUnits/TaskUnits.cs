using AgileBoardView;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BoardUnits
{
    [TestClass]
    public class TaskUnits
    {
        private static void AddEstimates()
        {
            if (Board.Estimates.Count == 0) {
                Board.Estimates.Add(1, new Estimate(1, "Low"));
                Board.Estimates.Add(2, new Estimate(2, "Medium"));
                Board.Estimates.Add(3, new Estimate(3, "High"));
            }
        }

        [TestMethod]
        public void TaskConstructor()
        {
            var e = new Estimate(0, "Low");
            var d = DateTime.Now.AddDays(30);
            var t = new Task("name", "description", e, d, 1, 2);

            Assert.AreEqual(t.Name, "name");
            Assert.AreEqual(t.Description, "description");
            Assert.AreEqual(t.Estimation, 0);
            Assert.AreEqual(t.TaskEndDate, d);
            Assert.AreEqual(t.columnId, 1);
            Assert.AreEqual(t.employId, 2);
        }
       
        [TestMethod]
        public void TaskEmptyConstructor()
        {
            AddEstimates();
            var t = new Task();

            Assert.AreEqual(t.Name, "Brak");
            Assert.AreEqual(t.Description, "Brak");
            Assert.AreEqual(t.Estimation, 1);
            Assert.AreEqual(t.columnId, 0);
            Assert.AreEqual(t.employId, 0);
        }
         
        [TestMethod]
        public void TaskConstructorWithTwoParameters()
        {
            AddEstimates();
            var t = new Task("name", "description");

            Assert.AreEqual(t.Name, "name");
            Assert.AreEqual(t.Description, "description");
            Assert.AreEqual(t.Estimation, 1);
            Assert.AreEqual(t.columnId, 0);
            Assert.AreEqual(t.employId, 0);
        }
        
        [TestMethod]
        public void TaskConstructorWithThreeParameters()
        {
            AddEstimates();
            var t = new Task("name", "description", Board.Estimates[1]);

            Assert.AreEqual(t.Name, "name");
            Assert.AreEqual(t.Description, "description");
            Assert.AreEqual(t.Estimation, 1);
            Assert.AreEqual(t.columnId, 0);
            Assert.AreEqual(t.employId, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TaskConstructorWithException()
        {
            var e = new Estimate(0, "Low");
            var d = DateTime.Now;
            new Task("name", "description", e, d, 1, 2);
        }
    }
}
