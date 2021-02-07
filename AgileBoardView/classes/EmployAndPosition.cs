namespace AgileBoardView
{
    ///   <summary>
    ///      Keeps information about Employ and his POsition in company
    ///      <example>
    ///        <code>
    ///             new EmployAndPosition(Employ employ, Position position);
    ///        </code>
    ///      </example>
    ///    </summary>
    public class EmployAndPosition
    {
        public Employ employ;
        public Position position;


        public EmployAndPosition(Employ employ, Position position)
        {
            this.position = position;
            this.employ = employ;
        }

        ///   <summary>
        ///      returns the employee's name
        ///    </summary>
        public string Name => employ.Name;

        ///   <summary>
        ///      returns the employee's surname
        ///    </summary>
        public string Surname => employ.Surname;

        ///   <summary>
        ///      returns the employee's position in company
        ///    </summary>
        public string Position => position.Name;
    }
}
