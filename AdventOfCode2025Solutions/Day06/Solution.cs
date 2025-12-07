using Common;
using System.Text;
using ToolsFramework;

namespace AdventOfCode2025Solutions.Day06
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 6: ";

        public string SolvePart1(string[] datasetLines)
        {
            var gridInitializerRows = datasetLines.Take(datasetLines.Length - 1).ToArray();
            var instructionString = datasetLines[datasetLines.Length - 1];
            WorkSheet worksheet = new(gridInitializerRows);

            //ConsolePrinterGridColumnRow.Print(worksheet.GetGridCells());
            //Console.WriteLine(instructionString);

            var resultOfInstructions = worksheet.ColumnCalculation(instructionString);
            //ConsolePrinter.Print(resultOfInstructions);

            var sum = resultOfInstructions.Sum();
            return sum.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var noOfRows = datasetLines.Length;
            var noOfColumns = datasetLines.Max(x => x.Length);

            Console.WriteLine($"No of Rows: {noOfRows}");
            Console.WriteLine($"No of Rows: {noOfColumns}");
            long sum = 0;

            List<long> factors = [];

            if (!TryGetOperation(datasetLines[noOfRows - 1][0], out OpType operationType))
                throw new Exception($"Operation string not found in column:0, row:{noOfRows - 1}");

            StringBuilder sb = new();

            for (var columnIndex = 0; columnIndex < noOfColumns; columnIndex++)
            {
                if (TryGetOperation(datasetLines[noOfRows - 1][columnIndex], out OpType ot))
                    operationType = ot;

                for (var rowIndex = 0; rowIndex < noOfRows; rowIndex++)
                {
                    sb.Append(datasetLines[rowIndex][columnIndex]);
                }


            }


            return "To be implemented";
        }

        internal static bool TryGetOperation(char c, out OpType opType)
        {
            opType = c switch
            {
                '+' => OpType.Addition,
                '-' => OpType.Subtract,
                '*' => OpType.Multiply,
                _ => OpType.None,
            };
            return opType != OpType.None;
        }
    }

    internal enum OpType
    {
        None,
        Subtract,
        Addition,
        Multiply,
        Division,
    }

    internal class Column(string[] datasetLines, int index, string operatorString)
    {

    }
}