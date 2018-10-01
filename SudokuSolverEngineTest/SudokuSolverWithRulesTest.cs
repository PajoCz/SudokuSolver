using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
using SudokuSolverEngine;
using SudokuSolverEngine.Domain;

namespace SudokuSolverEngineTest
{
    [TestFixture]
    public class SudokuSolverWithRulesTest
    {
        private const string LogDataToFileNamePattern = "D:\\SudokuLog-{0}.txt";
        private StringBuilder _LogData;

        [TestCase("3x9-WithSteps", true)]
        [TestCase("3x9-WithoutSteps", false)]
        public void Solve_3x9(string p_Description, bool p_LogOnCheckSolved)
        {
            _LogData = new StringBuilder();
            var boardData = new byte[,]
            {
                {0, 0, 0},
                {3, 0, 0},
                {0, 0, 1},
                {0, 0, 0},
                {0, 0, 0},
                {0, 3, 0},
                {0, 2, 6},
                {5, 0, 3},
                {4, 7, 0}
            };
            var boardSolved = BoardSolve(boardData, p_Description, p_LogOnCheckSolved);
            var boardExpected = new byte[,]
            {
                {2, 4, 5},
                {3, 6, 7},
                {8, 9, 1},
                {1, 5, 2},
                {6, 8, 4},
                {7, 3, 9},
                {9, 2, 6},
                {5, 1, 3},
                {4, 7, 8}
            };
            AssertBoard(boardSolved, boardExpected);
        }

        [TestCase("9x9LastNotFixed-WithSteps", true)]
        [TestCase("9x9LastNotFixed-WithoutSteps", false)]
        public void Solve_LastNotFixed_9x9(string p_Description, bool p_LogOnCheckSolved)
        {
            _LogData = new StringBuilder();
            var boardData = new byte[,]
            {
                {0, 0, 0, 2, 0, 0, 0, 6, 3},
                {3, 0, 0, 0, 0, 5, 4, 0, 1},
                {0, 0, 1, 0, 0, 3, 9, 8, 0},
                {0, 0, 0, 0, 0, 0, 0, 9, 0},
                {0, 0, 0, 5, 3, 8, 0, 0, 0},
                {0, 3, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 6, 3, 0, 0, 5, 0, 0},
                {5, 0, 3, 7, 0, 0, 0, 0, 8},
                {4, 7, 0, 0, 0, 1, 0, 0, 0}
            };
            var boardSolved = BoardSolve(boardData, p_Description, p_LogOnCheckSolved);
            var boardExpected = new byte[,]
            {
                {8, 5, 4, 2, 1, 9, 7, 6, 3},
                {3, 9, 7, 8, 6, 5, 4, 2, 1},
                {2, 6, 1, 4, 7, 3, 9, 8, 5},
                {7, 8, 5, 1, 2, 6, 3, 9, 4},
                {6, 4, 9, 5, 3, 8, 1, 7, 2},
                {1, 3, 2, 9, 4, 7, 8, 5, 6},
                {9, 2, 6, 3, 8, 4, 5, 1, 7},
                {5, 1, 3, 7, 9, 2, 6, 4, 8},
                {4, 7, 8, 6, 5, 1, 2, 3, 9}
            };
            AssertBoard(boardSolved, boardExpected);
        }

        [TestCase("9x9LastFixed-WithSteps", true)]
        [TestCase("9x9LastFixed-WithoutSteps", false)]
        public void Solve_LastFixed_9x9(string p_Description, bool p_LogOnCheckSolved)
        {
            _LogData = new StringBuilder();
            var boardData = new byte[,]
            {
                {0, 0, 0, 2, 0, 0, 0, 6, 3},
                {3, 0, 0, 0, 0, 5, 4, 0, 1},
                {0, 0, 1, 0, 0, 3, 9, 8, 0},
                {0, 0, 0, 0, 0, 0, 0, 9, 0},
                {0, 0, 0, 5, 3, 8, 0, 0, 0},
                {0, 3, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 6, 3, 0, 0, 5, 0, 0},
                {5, 0, 3, 7, 0, 0, 0, 0, 8},
                {4, 7, 0, 0, 0, 1, 0, 0, 9}
            };
            var boardSolved = BoardSolve(boardData, p_Description, p_LogOnCheckSolved);
            var boardExpected = new byte[,]
            {
                {8, 5, 4, 2, 1, 9, 7, 6, 3},
                {3, 9, 7, 8, 6, 5, 4, 2, 1},
                {2, 6, 1, 4, 7, 3, 9, 8, 5},
                {7, 8, 5, 1, 2, 6, 3, 9, 4},
                {6, 4, 9, 5, 3, 8, 1, 7, 2},
                {1, 3, 2, 9, 4, 7, 8, 5, 6},
                {9, 2, 6, 3, 8, 4, 5, 1, 7},
                {5, 1, 3, 7, 9, 2, 6, 4, 8},
                {4, 7, 8, 6, 5, 1, 2, 3, 9}
            };
            AssertBoard(boardSolved, boardExpected);
        }

        private static void AssertBoard(Board boardSolved, byte[,] boardExpected)
        {
            Assert.IsNotNull(boardSolved, "Board must be solved");
            Assert.IsTrue(boardSolved.SameData(boardExpected), "Board data another than expected");
        }

        private Board BoardSolve(byte[,] boardData, string p_Description, bool p_LogOnCheckSolved)
        {
            Board board = new Board(boardData);
            SudokuSolverBase solver = new SudokuSolverWithRules();
            if (p_LogOnCheckSolved)
            {
                solver.CheckRules += SolverOnCheckRules;
            }
            Stopwatch sw = Stopwatch.StartNew(); 
            Board boardSolved = solver.Solve(board);
            sw.Stop();

            if (boardSolved != null)
            {
                LogLine("BOARD SOLVED:");
                LogBoard(boardSolved);
            }
            LogLine($"CheckRulesForPositionCalled={solver.CheckRulesForPositionCalled}");
            LogLine($"CheckSolvedTime={sw.Elapsed}");

            File.WriteAllText(String.Format(LogDataToFileNamePattern, p_Description), _LogData.ToString());
            return boardSolved;
        }

        private void SolverOnCheckRules(Board p_Board, int? p_X, int? p_Y)
        {
            LogLine($"BOARD [{p_X},{p_Y}]:");
            LogBoard(p_Board);
        }

        private void LogBoard(Board p_Board)
        {
            for (int y = 0; y < p_Board.YLen; y++)
            {
                for (int x = 0; x < p_Board.XLen; x++)
                {
                    var item = p_Board.Data[y, x];
                    Log(item.FixedValue ? $"[{item.Value}] " : $"<{item.Value}> ");
                }
                LogLine("");
            }
        }

        private void Log(string p_Text)
        {
            _LogData.Append(p_Text);
            //Debug.Write(p_Text);
        }

        private void LogLine(string p_Text)
        {
            _LogData.AppendLine(p_Text);
            //Debug.WriteLine(p_Text);
        }
    }
}
