namespace ToolsFramework
{
    public sealed class WorkSheet
    {
        /// <summary>
        /// Provide strings that represent rows. 
        /// Must have equal length of columns in all rows.
        /// Consider this as being a configuration of cells of numbers like an Excel sheet
        /// 
        /// Numbers are separated by a full column of only spaces.
        /// The left/right alignment of numbers within each cell can be ignored.
        /// 
        /// 123 328  51 64 
        ///  45 64  387 23 
        ///   6 98  215 314
        /// </summary>
        /// <param name="initializeStrings"></param>
        public WorkSheet(string[] initializeStrings)
        {
            if (initializeStrings.Length == 0)
                throw new ArgumentException("No rows provided. Empty input.", nameof(initializeStrings));

            NumberOfRows = initializeStrings.Length;
            NumberOfColumns = ExtractNumbers(initializeStrings[0]).Length;

            _cells = new WorkSheetCell[NumberOfColumns, NumberOfRows];

            for (var row = 0; row < NumberOfRows; row++)
            {
                var numbers = ExtractNumbers(initializeStrings[row]);

                for (var column = 0; column < NumberOfColumns; column++)
                {
                    var longNumber = numbers[column];

                    var newCell = new WorkSheetCell(column, row, longNumber);
                    _cells[column, row] = newCell;
                }
            }
        }

        private readonly WorkSheetCell[,] _cells;

        public int NumberOfRows { get; private init; }
        public int NumberOfColumns { get; private init; }

        /// <summary>
        /// Get the cell value for given zero based index
        /// </summary>
        /// <param name="column">row index, zero based</param>
        /// <param name="row">column index, zero based</param>
        /// <returns>cell value, long number</returns>
        /// <exception cref="ArgumentOutOfRangeException">If any parameter are out of range in worksheet</exception>
        public long this[int column, int row]
        {
            get
            {
                CheckBoundaries(column, row);
                return _cells[column, row].Content;
            }
            set
            {
                CheckBoundaries(column, row);
                _cells[column, row].Content = value;
            }
        }

        /// <summary>
        /// Get the cell for given zero based index
        /// </summary>
        /// <param name="column">row index, zero based</param>
        /// <param name="row">column index, zero based</param>
        /// <returns>cell</returns>
        /// <exception cref="ArgumentOutOfRangeException">If any parameter are out of range in worksheet</exception>
        public WorkSheetCell GetCell(int column, int row)
        {
            CheckBoundaries(column, row);
            return _cells[column, row];
        }

        public WorkSheetCell[,] GetGridCells()
        {
            return _cells;
        }

        /// <summary>
        /// Make string of numbers separated by spaces into individual number array
        /// Exception thrown if any part of the string cannot convert to long
        /// </summary>
        /// <param name="txtString">input string</param>
        /// <returns>returned array. Empty if no numbers in the string</returns>
        private static long[] ExtractNumbers(string txtString)
        {
            var stringSplitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
            var strings = txtString.Split(' ', stringSplitOptions);
            var longs = strings.Select(long.Parse);
            return longs.ToArray();
        }

        /// <summary>
        /// Check boundaries. Throw exception if out of range.
        /// </summary>
        /// <param name="column">column index</param>
        /// <param name="row">row index</param>
        /// <exception cref="ArgumentOutOfRangeException">If any parameter are out of range in worksheet</exception>
        private void CheckBoundaries(int column, int row)
        {
            if (column >= NumberOfColumns)
                throw new ArgumentOutOfRangeException(nameof(column), $"Max column indexer is {NumberOfColumns - 1}");

            if (row >= NumberOfRows)
                throw new ArgumentOutOfRangeException(nameof(row), $"Max row indexer is {NumberOfRows - 1}");
        }

        /// <summary>
        /// Provide a sting that represent operations to perform on each colum
        /// ex. on 8 columns following are performed on all
        /// + + + * * + *
        /// </summary>
        /// <param name="instructionString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public long[] ColumnCalculation(string instructionString)
        {
            var stringSplitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
            var instructionStrings = instructionString.Split(' ', stringSplitOptions);

            if (instructionStrings.Length != NumberOfColumns)
                throw new ArgumentException("instruction string must have same length as columns", nameof(instructionString));

            var resultArray = new long[instructionStrings.Length];

            for (var column = 0; column < NumberOfColumns; column++)
            {
                var instruction = instructionStrings[column];
                long columnResult = _cells[column, 0].Content;

                for (var row = 0; row < NumberOfRows; row++)
                {
                    if (row == 0)
                        columnResult = _cells[column, row].Content;
                    else
                    {
                        if (instruction == "+")
                            columnResult += _cells[column, row].Content;
                        else if (instruction == "*")
                            columnResult *= _cells[column, row].Content;
                        else if (instruction == "-")
                            columnResult -= _cells[column, row].Content;
                        else
                            throw new Exception($"Unsupported operator: {instruction}");
                    }
                }
                resultArray[column] = columnResult;
            }
            return resultArray;
        }
    }

    public class WorkSheetCell(int columnIndex, int rowIndex, long c)
    {
        public long Content { get; set; } = c;
        public int ColumnIndex => columnIndex;
        public int RowIndex => rowIndex;
        public override string ToString() => Content.ToString();
    }
}