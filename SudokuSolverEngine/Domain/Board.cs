namespace SudokuSolverEngine.Domain
{
    public class Board
    {
        public int XLen => Data.GetLength(1);
        public int YLen => Data.GetLength(0);
        public readonly BoardItem[,] Data;

        public Board(byte[,] p_Values)
        {
            var xLen = p_Values.GetLength(1);
            var yLen = p_Values.GetLength(0);
            Data = new BoardItem[yLen, xLen];
            for (int y = 0; y < p_Values.GetLength(0); y++)
            for (int x = 0; x < p_Values.GetLength(1); x++)
            {
                Data[y, x] = new BoardItem(p_Values[y, x], p_Values[y, x] != 0);
            }
        }

        public Board(BoardItem[,] p_OriginalData)
        {
            Data = p_OriginalData;
        }

        public Board CloneDeep(Board p_Board)
        {
            BoardItem[,] result = new BoardItem[p_Board.YLen, p_Board.XLen];
            for (int y = 0; y < p_Board.YLen; y++)
            for (int x = 0; x < p_Board.XLen; x++)
            {
                    result[y, x] = new BoardItem(p_Board.Data[y, x].Value, p_Board.Data[y, x].FixedValue);
            }
            return new Board(result);
        }

        public bool SameData(byte[,] p_Data)
        {
            if (p_Data == null || p_Data.GetLength(0) != Data.GetLength(0) || p_Data.GetLength(1) != Data.GetLength(1)) return false;
            for (int y = 0; y <= p_Data.GetLength(0) - 1; y++)
            for (int x = 0; x <= p_Data.GetLength(1) - 1; x++)
            {
                if (p_Data[y, x] != Data[y, x].Value)
                    return false;
            }
            return true;
        }

        public bool LastIndexAtData(int p_Index)
        {
            return p_Index == Data.Length - 1;
        }
    }
}