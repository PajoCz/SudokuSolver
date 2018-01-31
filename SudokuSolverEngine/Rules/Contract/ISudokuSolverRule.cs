using SudokuSolverEngine.Domain;

namespace SudokuSolverEngine.Rules.Contract
{
    public interface ISudokuSolverRule
    {
        bool CheckRuleForPosition(Board p_Board, int p_X, int p_Y);
    }
}