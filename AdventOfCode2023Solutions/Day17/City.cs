using System.Linq;
using System.Reflection;

namespace AdventOfCode2023Solutions.Day17
{
    internal class City
    {
        private readonly int[,] map;
        private readonly int noOfRows;
        private readonly int noOfCols;
        private readonly int destinationRow;
        private readonly int destinationCol;
        internal int lowestFoundAccumulatedHeatCost;
        internal long routesToDestinationFound;

        public City(string[] datasetLines)
        {
            noOfRows = datasetLines.Length;
            noOfCols = datasetLines[0].Length;
            destinationRow = noOfRows - 1;
            destinationCol = noOfCols - 1;
            map = CreateMap(datasetLines);
            lowestFoundAccumulatedHeatCost = int.MaxValue;
            routesToDestinationFound = 0;
        }

        public void TestRoutesThroughCity()
        {
            int col = 0;
            int row = 0;
            int heat = 0;
            int directionCounter = 1;
            string moveHistory = AddToMoveHistory(row, col, "");

            MoveIntoField(row, col + 1, Direction.Rightward, directionCounter, heat, moveHistory);
            MoveIntoField(row + 1, col, Direction.Downward, directionCounter, heat, moveHistory);
        }

        private string AddToMoveHistory(int row, int col, string history) 
        {
            return history + " " + GetFieldNo(row, col);
        }

        private int GetFieldNo(int row, int col)
        {
            return (row * noOfCols) + col;
        }

        private bool CheckAlreadyBeenThere(int row, int col, string moveHistory)
        {
            int fieldNo = GetFieldNo(row, col);
            return moveHistory.Contains(fieldNo.ToString());
        }

        private void MoveIntoField(int row, int col, Direction direction, int sameDirectionCounter, int accumulatedHeatCost, string moveHistory)
        {
            if (row < 0 || row >= noOfRows || col < 0 || col >= noOfCols)
                return;

            if (sameDirectionCounter == 4)
                return;

            if (CheckAlreadyBeenThere(row, col, moveHistory))
                return;

            accumulatedHeatCost += map[row, col];
            moveHistory = AddToMoveHistory(row, col, moveHistory);

            if (row == destinationRow && col == destinationCol)
            {
                lowestFoundAccumulatedHeatCost = Math.Min(lowestFoundAccumulatedHeatCost, accumulatedHeatCost);
                routesToDestinationFound++;
                return;
            }

            //Official rules: Not allowed to move same direction more than 3 times in a row
            //Official rules: Not allowed to move back the way you came from
            //My extra rules: Not allowed to go left if you go up - since that is opposite of destination
            //My extra rules: Not allowed to go up after going right - since that is opposite of destination

            switch(direction)
            {
                case Direction.Upward:
                    MoveUpward(row, col, ++sameDirectionCounter, accumulatedHeatCost, moveHistory);
                    MoveRightward(row, col, 1, accumulatedHeatCost, moveHistory);
                    break;

                case Direction.Leftward:
                    MoveLeftward(row, col, ++sameDirectionCounter, accumulatedHeatCost, moveHistory);
                    MoveDownward(row, col, 1, accumulatedHeatCost, moveHistory);
                    break;

                case Direction.Downward:
                    MoveLeftward(row, col, 1, accumulatedHeatCost, moveHistory);
                    MoveDownward(row, col, ++sameDirectionCounter, accumulatedHeatCost, moveHistory);
                    MoveRightward(row, col, 1, accumulatedHeatCost, moveHistory);
                    break;

                case Direction.Rightward:
                    MoveUpward(row, col, 1, accumulatedHeatCost, moveHistory);
                    MoveRightward(row, col, ++sameDirectionCounter, accumulatedHeatCost, moveHistory);
                    MoveDownward(row, col, 1, accumulatedHeatCost, moveHistory);
                    break;
            }
        }

        private void MoveRightward(int fromRow, int fromCol, int sameDirCount, int heatAccum, string moveHist) => MoveIntoField(fromRow, fromCol + 1, Direction.Rightward, sameDirCount, heatAccum, moveHist);
        private void MoveLeftward(int fromRow, int fromCol, int sameDirCount, int heatAccum, string moveHist) => MoveIntoField(fromRow, fromCol - 1, Direction.Leftward, sameDirCount, heatAccum, moveHist);
        private void MoveUpward(int fromRow, int fromCol, int sameDirCount, int heatAccum, string moveHist) => MoveIntoField(fromRow - 1, fromCol, Direction.Upward, sameDirCount, heatAccum, moveHist);
        private void MoveDownward(int fromRow, int fromCol, int sameDirCount, int heatAccum, string moveHist) => MoveIntoField(fromRow + 1, fromCol, Direction.Downward, sameDirCount, heatAccum, moveHist);


        private static int[,] CreateMap(string[] datasetLines)
        {
            int noOfRows = datasetLines.Length;
            int noOfCols = datasetLines[0].Length;
            var map = new int[noOfRows, noOfCols];
            for (int columnIndex = 0; columnIndex < noOfCols; columnIndex++)
                for (int rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                    map[rowIndex, columnIndex] = (int)char.GetNumericValue(datasetLines[rowIndex][columnIndex]);
            return map;
        }
    }
}
