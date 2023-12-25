namespace AdventOfCode2023Solutions.Day16
{
    internal class Contraption
    {
        private readonly char[,] map;
        private readonly string[,] energyMap;
        private readonly int noOfRows;
        private readonly int noOfCols;

        public Contraption(string[] datasetLines)
        {
            noOfRows = datasetLines.Length;
            noOfCols = datasetLines[0].Length;
            map = CreateMap(datasetLines);
            energyMap = CreateEnergyMap(noOfRows, noOfCols);
        }

        public int CountEnergizedFields() { return energyMap.Cast<string>().Count(f => f != string.Empty); }

        public void BeamIntoField(int row, int col, Direction direction)
        {
            if (row < 0 || row >= noOfRows || col < 0 || col >= noOfCols)
                return;

            var fieldAlreadyContainsSameBeamDirection = AddEnergyField(row, col, direction);
            if (fieldAlreadyContainsSameBeamDirection)
                return;

            var gridField = map[row, col];

            if (gridField == '.')
            {
                if (direction == Direction.Rightward) BeamRightward(row, col);
                else if (direction == Direction.Leftward) BeamLeftward(row, col);
                else if (direction == Direction.Upward) BeamUpward(row, col);
                else if (direction == Direction.Downward) BeamDownward(row, col);
            }
            else if (gridField == '/')
            {
                if (direction == Direction.Rightward) BeamUpward(row, col);
                else if (direction == Direction.Leftward) BeamDownward(row, col);
                else if (direction == Direction.Upward) BeamRightward(row, col);
                else if (direction == Direction.Downward) BeamLeftward(row, col);
            }
            else if (gridField == '\\')
            {
                if (direction == Direction.Rightward) BeamDownward(row, col);
                else if (direction == Direction.Leftward) BeamUpward(row, col);
                else if (direction == Direction.Upward) BeamLeftward(row, col);
                else if (direction == Direction.Downward) BeamRightward(row, col);
            }
            else if (gridField == '|')
            {
                if (direction == Direction.Rightward || direction == Direction.Leftward)
                {
                    BeamDownward(row, col);
                    BeamUpward(row, col);
                }
                else if (direction == Direction.Upward) BeamUpward(row, col);
                else if (direction == Direction.Downward) BeamDownward(row, col);
            }
            else if (gridField == '-')
            {
                if (direction == Direction.Rightward) BeamRightward(row, col);
                else if (direction == Direction.Leftward) BeamLeftward(row, col);
                else if (direction == Direction.Upward || direction == Direction.Downward)
                {
                    BeamRightward(row, col);
                    BeamLeftward(row, col);
                }
            }
        }

        private void BeamRightward(int fromRow, int fromCol) => BeamIntoField(fromRow, fromCol + 1, Direction.Rightward);
        private void BeamLeftward(int fromRow, int fromCol) => BeamIntoField(fromRow, fromCol - 1, Direction.Leftward);
        private void BeamUpward(int fromRow, int fromCol) => BeamIntoField(fromRow - 1, fromCol, Direction.Upward);
        private void BeamDownward(int fromRow, int fromCol) => BeamIntoField(fromRow + 1, fromCol, Direction.Downward);

        private bool AddEnergyField(int row, int col, Direction direction)
        {
            if (energyMap[row, col].Contains((char)direction))
                return true;
            energyMap[row, col] += (char)direction;
            return false;
        }

        public static char[,] CreateMap(string[] datasetLines)
        {
            int noOfRows = datasetLines.Length;
            int noOfCols = datasetLines[0].Length;
            char[,] map = new char[noOfRows, noOfCols];
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                    map[rowIndex, columnIndex] = datasetLines[rowIndex][columnIndex];
            return map;
        }

        public static string[,] CreateEnergyMap(int noOfRows, int noOfCols)
        {
            var map = new string[noOfRows, noOfCols];
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                    map[rowIndex, columnIndex] = "";
            return map;
        }
    }
}
