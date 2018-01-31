using SudokuSolverEngine.Domain;
using SudokuSolverEngine.Rules.Contract;

namespace SudokuSolverEngine.Rules.Service
{
    public class SudokuSolverRuleColumnUniqueValues : ISudokuSolverRule
    {
        public bool CheckRuleForPosition(Board p_Board, int p_X, int p_Y)
        {
            var value = p_Board.Data[p_Y, p_X].Value;
            for (int y = 0; y < p_Board.YLen; y++)
            {
                if (y == p_Y) continue;
                if (p_Board.Data[y, p_X].Value == value) return false;
            }
            return true;
        }

        //public bool CheckRuleForBoard(Board p_Board)
        //{
        //    for (int x = 0; x < p_Board.Data.GetLength(1); x++)
        //    for (int y1 = 0; y1 < p_Board.Data.GetLength(0) - 1; y1++)
        //    for (int y2 = y1 + 1; y2 < p_Board.Data.GetLength(0); y2++)
        //    {
        //        if (p_Board.Data[y1, x].Value == p_Board.Data[y2, x].Value) return false;
        //    }
        //    return true;
        //}
    }
}