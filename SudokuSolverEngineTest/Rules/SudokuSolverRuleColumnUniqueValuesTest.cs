using System;
using NUnit.Framework;
using SudokuSolverEngine.Domain;
using SudokuSolverEngine.Rules.Service;

namespace SudokuSolverEngineTest.Rules
{
    [TestFixture]
    public class SudokuSolverRuleColumnUniqueValuesTest
    {
        [Test]
        public void CheckRuleForPosition_OneColumn_ReturnsTrue()
        {
            var board = new Board(new byte[,]
            {
                {1},
                {2},
                {3},
                {4},
                {5},
                {6},
                {7},
                {8},
                {9},
            });
            Assert.IsTrue(new SudokuSolverRuleColumnUniqueValues().CheckRuleForPosition(board, 0, 3));

            board = new Board(new byte[,]
            {
                {1},
                {1},
                {1},
                {2},
                {1},
                {1},
                {1},
                {1},
                {1}
            });
            Assert.IsTrue(new SudokuSolverRuleColumnUniqueValues().CheckRuleForPosition(board, 0, 3));
        }

        [Test]
        public void CheckRuleForPosition_MoreColumns_ReturnsTrue()
        {
            var board = new Board(new byte[,]
            {
                {1, 5},
                {2, 5},
                {3, 5},
                {4, 5},
                {5, 5},
                {6, 5},
                {7, 5},
                {8, 5},
                {9, 5},
            });
            Assert.IsTrue(new SudokuSolverRuleColumnUniqueValues().CheckRuleForPosition(board, 0, 3));

            board = new Board(new byte[,]
            {
                {5, 1},
                {5, 1},
                {5, 1},
                {5, 2},
                {5, 1},
                {5, 1},
                {5, 1},
                {5, 1},
                {5, 1}
            });
            Assert.IsTrue(new SudokuSolverRuleColumnUniqueValues().CheckRuleForPosition(board, 1, 3));
        }

        [Test]
        public void CheckRuleForPosition_OneColumn_ReturnsFalse()
        {
            var board = new Board(new byte[,]
            {
                {1},
                {2},
                {3},
                {4},
                {5},
                {6},
                {7},
                {8},
                {4},
            });
            Assert.IsFalse(new SudokuSolverRuleColumnUniqueValues().CheckRuleForPosition(board, 0, 3));

            board = new Board(new byte[,]
            {
                {1},
                {1},
                {1},
                {2},
                {1},
                {1},
                {1},
                {1},
                {2}
            });
            Assert.IsFalse(new SudokuSolverRuleColumnUniqueValues().CheckRuleForPosition(board, 0, 3));
        }

        [Test]
        public void CheckRuleForPosition_MoreColumns_ReturnsFalse()
        {
            var board = new Board(new byte[,]
            {
                {1, 1},
                {2, 2},
                {3, 3},
                {4, 4},
                {5, 5},
                {6, 6},
                {7, 7},
                {8, 8},
                {4, 9},
            });
            Assert.IsFalse(new SudokuSolverRuleColumnUniqueValues().CheckRuleForPosition(board, 0, 3));

            board = new Board(new byte[,]
            {
                {1, 1},
                {2, 1},
                {3, 1},
                {4, 2},
                {5, 1},
                {6, 1},
                {7, 1},
                {8, 1},
                {9, 2}
            });
            Assert.IsFalse(new SudokuSolverRuleColumnUniqueValues().CheckRuleForPosition(board, 1, 3));
        }
    }
}