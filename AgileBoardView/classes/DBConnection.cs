using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileBoardView
{
    public class BoardDB
    {
        private static AgileBoardDB db = null;
        private static void CheckDBConnection()
        {
            if (BoardDB.db is null)
                BoardDB.db = new AgileBoardDB();
        }

        public static AgileBoardDB GetDB() {
            BoardDB.CheckDBConnection();

            return BoardDB.db;
        }

        public static long GetColumnId(BoardColumns column) => BoardDB.GetDB().Columns.FirstOrDefault(p => p.Name == BoardConst.BoardColumnsNames[column]).columnId;
    }
}
