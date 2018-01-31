using SudokuSolverEngine.Domain;
using SudokuSolverEngine.Rules.Contract;

namespace SudokuSolverEngine.Rules.Service
{
    public class SudokuSolverRuleRowUniqueValues : ISudokuSolverRule
    {
        public bool CheckRuleForPosition(Board p_Board, int p_X, int p_Y)
        {
            var value = p_Board.Data[p_Y, p_X].Value;
            for (int x = 0; x < p_Board.XLen; x++)
            {
                if (x == p_X) continue;
                if (p_Board.Data[p_Y, x].Value == value) return false;
            }
            return true;
        }

        //public bool CheckRuleForBoard(Board p_Board)
        //{
        //    for (int y = 0; y < p_Board.Data.GetLength(0); y++)
        //    for (int x1 = 0; x1 < p_Board.Data.GetLength(1) - 1; x1++)
        //    for (int x2 = x1 + 1; x2 < p_Board.Data.GetLength(1); x2++)
        //    {
        //        if (p_Board.Data[y, x1].Value == p_Board.Data[y, x2].Value) return false;
        //    }
        //    return true;
        //}
    }
}