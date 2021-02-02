using System;

namespace AgileBoardView
{
    ///   <summary>
    ///      Keeps information about Employ assigned to Task 
    ///      <example>
    ///        <code>
    ///             new TaskAndEmploy(Task task, Employ employ);
    ///        </code>
    ///      </example>
    ///    </summary>
    public class TaskAndEmploy
    {
        public Task task;
        public Employ employ;

        public TaskAndEmploy(Task task, Employ employ)
        {
            this.task = task;
            this.employ = employ;
        }

        ///   <summary>
        ///      returns the task's name
        ///    </summary>
        public string Name => task.Name;

        ///   <summary>
        ///      returns the task's description
        ///    </summary>
        public string Description => task.Description;

        ///   <summary>
        ///      returns the Estimates's name
        ///    </summary>
        public string Estimation => Board.Estimates[task.Estimation].Name;

        ///   <summary>
        ///      returns the AddToBoard Date 
        ///    </summary>
        public DateTime AddToBoardDate => task.AddToBoardDate;

        ///   <summary>
        ///      returns the LastModify Date 
        ///    </summary>
        public DateTime LastModifyDate => task.LastModifyDate;

        ///   <summary>
        ///      returns the TaskEnd  Date 
        ///    </summary>
        public DateTime TaskEndDate => task.TaskEndDate;

        ///   <summary>
        ///      returns the Employ assigned to Task 
        ///    </summary>
        public string Assign => employ.ToString();
    }
}
