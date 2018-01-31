using System;
using NUnit.Framework;
using SudokuSolverEngine.Domain;
using SudokuSolverEngine.Rules.Service;

namespace SudokuSolverEngineTest.Rules
{
    [TestFixture]
    public class SudokuSolverRuleSquareUniqueValuesTest
    {
        [Test]
        public void CheckRuleForBoard_BadBoardDimensions_ThrowsException()
        {
            var board = new Board(new byte[,]
            {
                { 1, 2, 3 }
            });
            Assert.Catch<IndexOutOfRangeException>(() => new SudokuSolverRuleSquareUniqueValues().CheckRuleForPosition(board, 0, 0));

            board = new Board(new byte[,]
            {
                { 1 },
                { 2 },
                { 3 }
            });
            Assert.Catch<IndexOutOfRangeException>(() => new SudokuSolverRuleSquareUniqueValues().CheckRuleForPosition(board, 0, 0));
        }

        [Test]
        public void CheckRuleForBoard_Board6x3_CheckValueFromFirstSquare_ReturnTrue()
        {
            var board = new Board(new byte[,]
            {
                { 1, 2, 3, 3, 2, 1 },
                { 4, 5, 6, 6, 5, 4},
                { 7, 8, 9, 9, 8, 3 }
            });
            Assert.IsTrue(new SudokuSolverRuleSquareUniqueValues().CheckRuleForPosition(board, 0, 0));
        }

        [Test]
        public void CheckRuleForBoard_Board6x3_CheckValueFromSecondSquare_ReturnFalse()
        {
            var board = new Board(new byte[,]
            {
                { 1, 2, 3, 3, 2, 1 },
                { 4, 5, 6, 6, 5, 4},
                { 7, 8, 9, 9, 8, 3 }
            });
            Assert.IsFalse(new SudokuSolverRuleSquareUniqueValues().CheckRuleForPosition(board, 3, 0));
        }
    }
}