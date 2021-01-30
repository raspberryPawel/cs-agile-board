using System;

namespace AgileBoardView
{
    public class TaskAndEmploy
    {
        public Task task;
        public Employ employ;

        public TaskAndEmploy(Task task, Employ employ)
        {
            this.task = task;
            this.employ = employ;
        }

        public string Name => task.Name;
        public string Description => task.Description;
        public string Estimation => Board.Estimates[task.Estimation].Name;
        public DateTime AddToBoardDate => task.AddToBoardDate;
        public DateTime LastModifyDate => task.LastModifyDate;
        public DateTime TaskEndDate => task.TaskEndDate;
        public string Assign => employ.ToString();
    }
}
