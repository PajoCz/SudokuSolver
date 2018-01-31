using SudokuSolverEngine.Domain;
using SudokuSolverEngine.Rules.Contract;

namespace SudokuSolverEngine.Rules.Service
{
    public class SudokuSolverRuleRowUniqueValues : ISudokuSolverRule
    {
        public bool CheckRuleForPosition(Board p_Board, int p_X, int p_Y)
        {
            var value = p_Board.Data[p_Y, p_X].Value;
            for (var x = 0; x < p_Board.XLen; x++)
            {
                if (x == p_X) continue;
                if (p_Board.Data[p_Y, x].Value == value) return false;
            }
            return true;
        }
    }
}