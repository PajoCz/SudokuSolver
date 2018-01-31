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
    }
}