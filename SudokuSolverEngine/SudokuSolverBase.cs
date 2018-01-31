using System.Collections.Generic;
using System.Linq;
using SudokuSolverEngine.Domain;
using SudokuSolverEngine.Rules.Contract;

namespace SudokuSolverEngine
{
    public class SudokuSolverBase
    {
        public int CheckRulesForPositionCalled { get; private set; }
        private readonly List<ISudokuSolverRule> _Rules;

        public SudokuSolverBase(IEnumerable<ISudokuSolverRule> p_Rules)
        {
            _Rules = p_Rules?.ToList();
        }

        public delegate void CheckRulesDelegate(Board p_Board, int? p_X, int? p_Y);
        public event CheckRulesDelegate CheckRules;

        private Board _Original;
        public Board Solve(Board p_Board)
        {
            CheckRulesForPositionCalled = 0;
            _Original = p_Board;
            var solving = p_Board.CloneDeep(_Original);
            bool found = CheckSolvedOrSwitchToNextPossibleState(solving, 0);
            return found ? solving : null;
        }

        private bool CheckSolvedOrSwitchToNextPossibleState(Board p_Board, int p_IndexWhereMayIncrementValueIfNotFixed)
        {
            int x = p_IndexWhereMayIncrementValueIfNotFixed % p_Board.XLen;
            int y = p_IndexWhereMayIncrementValueIfNotFixed / p_Board.XLen;

            if (!p_Board.Data[y, x].FixedValue)
            {   //NOT FIXED VALUE
                while (p_Board.Data[y, x].Value <= 9)
                {
                    if (p_Board.Data[y, x].Value > 0 && CheckRulesForPosition(p_Board, x, y))
                    {
                        if (SolvedAtIndexSoCheckLastIndexOrCallNextIndexRecursively(p_Board, p_IndexWhereMayIncrementValueIfNotFixed))
                            return true;
                    }
                    p_Board.Data[y, x].ValueIncrement();
                }
                p_Board.Data[y, x].ValueToZero();
            }
            else
            {   //FIXED VALUE
                if (SolvedAtIndexSoCheckLastIndexOrCallNextIndexRecursively(p_Board, p_IndexWhereMayIncrementValueIfNotFixed))
                    return true;
            }

            return false;
        }

        private bool SolvedAtIndexSoCheckLastIndexOrCallNextIndexRecursively(Board p_Board, int p_IndexWhereMayIncrementValueIfNotFixed)
        {
            if (p_Board.LastIndexAtData(p_IndexWhereMayIncrementValueIfNotFixed))
                return true;

            if (CheckSolvedOrSwitchToNextPossibleState(p_Board, p_IndexWhereMayIncrementValueIfNotFixed + 1))
            {
                return true;
            }

            return false;
        }

        private bool CheckRulesForPosition(Board p_Board, int p_X, int p_Y)
        {
            CheckRulesForPositionCalled++;
            CheckRules?.Invoke(p_Board, p_X, p_Y);
            return _Rules?.TrueForAll(r => r.CheckRuleForPosition(p_Board, p_X, p_Y)) ?? false;
        }
    }
}