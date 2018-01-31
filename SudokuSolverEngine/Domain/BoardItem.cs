using System;

namespace SudokuSolverEngine.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class BoardItem
    {
        private byte _Value;
        public byte Value
        {
            get => _Value;
            set
            {
                if (FixedValue) throw new ArgumentException("Fixed BoardItem.Value can't be modified");
                _Value = value;
            }
        }

        public bool FixedValue { get; set; }

        public BoardItem(byte p_Value, bool p_FixedValue)
        {
            if (p_Value > 9) throw new ArgumentOutOfRangeException(nameof(p_Value));
            if (p_Value == 0 && p_FixedValue) throw new ArgumentOutOfRangeException($"{nameof(p_Value)} cant be zero if {nameof(p_FixedValue)}=true");

            Value = p_Value;
            FixedValue = p_FixedValue;
        }

        public void ValueToZero()
        {
            Value = 0;
        }

        public void ValueIncrement()
        {
            Value++;
        }
    }
}