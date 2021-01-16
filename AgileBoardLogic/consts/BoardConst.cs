using System.Collections.Generic;

namespace AgileBoardLogic
{
    class BoardConst
    {
        public enum BoardColumns { Open, Coding, Test, Resolve };
        public static Dictionary<BoardColumns, string> BoardColumnsNames = new Dictionary<BoardColumns, string>()
        {
            [BoardColumns.Open] = "Open",
            [BoardColumns.Coding] = "Coding",
            [BoardColumns.Test] = "Test",
            [BoardColumns.Resolve] = "Resolve",
        };
    }
}
