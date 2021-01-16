using System.Collections.Generic;

namespace AgileBoardLogic
{
    public enum BoardColumns { Open, Coding, Test, Resolve, Any };
    public class BoardConst
    {
        public static Dictionary<BoardColumns, string> BoardColumnsNames = new Dictionary<BoardColumns, string>()
        {
            [BoardColumns.Open] = "Open",
            [BoardColumns.Coding] = "Coding",
            [BoardColumns.Test] = "Test",
            [BoardColumns.Resolve] = "Resolve",
        };
    }
}
