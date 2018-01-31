using System;
using NUnit.Framework;
using SudokuSolverEngine.Domain;
using SudokuSolverEngine.Rules.Service;

namespace SudokuSolverEngineTest.Rules
{
    [TestFixture]
    public class SudokuSolverRuleRowUniqueValuesTest
    {
        [Test]
        public void CheckRuleForPosition_OneRow_ReturnsTrue()
        {
            var board = new Board(new byte[,]
            {
                {2,1,4,3,9,8,7,6,5},
            });
            Assert.IsTrue(new SudokuSolverRuleRowUniqueValues().CheckRuleForPosition(board, 3, 0));

            board = new Board(new byte[,]
            {
                {1,1,1,2,1,1,1,1},
            });
            Assert.IsTrue(new SudokuSolverRuleRowUniqueValues().CheckRuleForPosition(board, 3, 0));
        }

        [Test]
        public void CheckRuleForPosition_MoreRows_ReturnsTrue()
        {
            var board = new Board(new byte[,]
            {
                {2,1,4,3,9,8,7,6,5},
                {1,1,1,1,1,1,1,1,1},
            });
            Assert.IsTrue(new SudokuSolverRuleRowUniqueValues().CheckRuleForPosition(board, 3, 0));

            board = new Board(new byte[,]
            {
                {1,1,1,1,1,1,1,1},
                {1,1,1,2,1,1,1,1},
            });
            Assert.IsTrue(new SudokuSolverRuleRowUniqueValues().CheckRuleForPosition(board, 3, 1));
        }

        [Test]
        public void CheckRuleForPosition_OneRow_ReturnsFalse()
        {
            var board = new Board(new byte[,]
            {
                {2,1,4,3,9,8,7,6,3},
            });
            Assert.IsFalse(new SudokuSolverRuleRowUniqueValues().CheckRuleForPosition(board, 3, 0));

            board = new Board(new byte[,]
            {
                {2,1,1,2,1,1,1,1},
            });
            Assert.IsFalse(new SudokuSolverRuleRowUniqueValues().CheckRuleForPosition(board, 3, 0));
        }

        [Test]
        public void CheckRuleForPosition_MoreRows_ReturnsFalse()
        {
            var board = new Board(new byte[,]
            {
                {2,1,4,3,9,8,7,6,3},
                {1,2,3,4,5,6,7,8,9},
            });
            Assert.IsFalse(new SudokuSolverRuleRowUniqueValues().CheckRuleForPosition(board, 3, 0));

            board = new Board(new byte[,]
            {
                {1,2,3,4,5,6,7,8,9},
                {2,1,1,2,1,1,1,1,1},
            });
            Assert.IsFalse(new SudokuSolverRuleRowUniqueValues().CheckRuleForPosition(board, 3, 1));
        }
    }
}