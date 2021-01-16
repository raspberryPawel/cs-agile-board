using System;

namespace AgileBoardLogic
{
	public class Task
	{
		#region fields
		public string Name { get; set; }
		public string Description { get; set; }
		public Estimate Estimation { get; set; }
		public DateTime AddToBoardDate { get; private set; }
		public DateTime LastModifyDate { get; set; }
		public DateTime TaskEndDate { get; set; }
        
		#endregion

        #region constructors
        public Task() : this("Brak", "Brak", Estimate.Low, DateTime.Now.AddDays(30)) { }
		public Task(string name, string description) : this(name, description, Estimate.Low, DateTime.Now.AddDays(30)) { }
		public Task(string name, string description, Estimate estimation) : this(name, description, estimation, DateTime.Now.AddDays(30)) { }
        public Task(string name, string description, Estimate estimation, DateTime taskEndDate)
        {
			Name = name;
			Description = description;
            Estimation = estimation;
            TaskEndDate = taskEndDate;

			AddToBoardDate = DateTime.Now;
			LastModifyDate = DateTime.Now;

			this.ValidateDates(AddToBoardDate, TaskEndDate);
		}
		#endregion

		private void ValidateDates(DateTime first, DateTime second) {
			if (second < first) { throw new InvalidOperationException("End date must be grater than start date"); }
		}
		public override string ToString()  => $"{Name}";
	}
}
