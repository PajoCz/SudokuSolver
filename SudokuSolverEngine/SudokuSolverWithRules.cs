using System.Collections.Generic;
using SudokuSolverEngine.Rules.Contract;
using SudokuSolverEngine.Rules.Service;

namespace SudokuSolverEngine
{
    public class SudokuSolverWithRules : SudokuSolverBase
    {
        public SudokuSolverWithRules() : base(new List<ISudokuSolverRule>
        {
            new SudokuSolverRuleRowUniqueValues(),
            new SudokuSolverRuleColumnUniqueValues(),
            new SudokuSolverRuleSquareUniqueValues()
        })
        {
        }
    }
}