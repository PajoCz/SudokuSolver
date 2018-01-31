using SudokuSolverEngine.Domain;
using SudokuSolverEngine.Rules.Contract;

namespace SudokuSolverEngine.Rules.Service
{
    public class SudokuSolverRuleSquareUniqueValues : ISudokuSolverRule
    {
        public bool CheckRuleForPosition(Board p_Board, int p_X, int p_Y)
        {
            var value = p_Board.Data[p_Y, p_X].Value;
            const int SquareSize = 3;
            var startX = p_X / SquareSize * SquareSize;
            var startY = p_Y / SquareSize * SquareSize;
            for (var offsetX = 0; offsetX < SquareSize; offsetX++)
            for (var offsetY = 0; offsetY < SquareSize; offsetY++)
            {
                var positionX = startX + offsetX;
                var positionY = startY + offsetY;
                if (p_X == positionX && p_Y == positionY) continue;
                if (p_Board.Data[positionY, positionX].Value == value) return false;
            }
            return true;
        }
    }
}