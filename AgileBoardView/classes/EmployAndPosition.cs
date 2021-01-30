using System;


namespace AgileBoardView
{
    public class EmployAndPosition
    {
        public Employ employ;
        public Position position;


        public EmployAndPosition(Employ employ, Position position)
        {
            this.position = position;
            this.employ = employ;
        }

        public string Name => employ.Name;
        public string Surname => employ.Surname;
        public string Position => position.Name;
    }
}
