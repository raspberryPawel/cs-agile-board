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

        public static Dictionary<Estimate, string> BoardEstimation = new Dictionary<Estimate, string>()
        {
            [Estimate.Low] = "Low",
            [Estimate.Medium] = "Medium",
            [Estimate.High] = "High",
            [Estimate.Critical] = "Critical",
        };
    }
}
