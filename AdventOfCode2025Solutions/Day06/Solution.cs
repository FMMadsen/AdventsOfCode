using Common;
using System.Text;
using ToolsFramework;

namespace AdventOfCode2025Solutions.Day06
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 6: Trash Compactor";

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
            var noOfDataRows = noOfRows - 1;
            var operationRow = noOfRows - 1;
            var noOfColumns = datasetLines.Max(x => x.Length);

            //Console.WriteLine($"No of Rows: {noOfRows}");
            //Console.WriteLine($"No of Columns: {noOfColumns}");

            List<long> problemResults = [];
            List<long> problemFactors = [];

            StringBuilder numberBuilder = new();
            MathOperationTypes operationType = MathOperationTypes.None;

            for (var columnIndex = 0; columnIndex < noOfColumns; columnIndex++)
            {
                char operatorChar = datasetLines[operationRow][columnIndex];
                if (OperationExtensions.TryFromChar(operatorChar, out MathOperationTypes opType))
                {
                    //Operator found - meaning new calculation problem identified. Finalize current:
                    FinalizeProblem(problemResults, problemFactors, operationType);
                    problemFactors.Clear();
                    operationType = opType;
                }

                //Build number out of column
                numberBuilder.Clear();
                for (var rowIndex = 0; rowIndex < noOfDataRows; rowIndex++)
                {
                    numberBuilder.Append(datasetLines[rowIndex][columnIndex]);
                }
                var number = NumberTools.ConstructNumberLong(numberBuilder);

                //if (columnIndex < 10)
                    //Console.WriteLine($"Column {columnIndex}: {number}");

                //Add number to the problem factors
                if (number != 0)
                    problemFactors.Add(number);

                if(columnIndex == noOfColumns-1)    //We reached final column, now finalize last problem
                    FinalizeProblem(problemResults, problemFactors, operationType);
            }

            var sum = problemResults.Sum();
            return sum.ToString();
        }

        private static void FinalizeProblem(List<long> problemResults, List<long> problemFactors, MathOperationTypes operationType)
        {
            //First calculate current problem to finalize
            var problemResult = NumberTools.OperateOnArray(problemFactors.ToArray(), operationType);
            problemResults.Add(problemResult);

            //Console.WriteLine($"Problem Result: {problemResult} Op:({operationType})");
        }
    }
}