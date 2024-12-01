using Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023Solutions.Day12
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 12: Hot Springs";

        public string SolvePart1(string[] datasetLines)
        {
            /*
            SpringStatusSheet3[] springStatusSheets = BuildSpringStatusSheets(datasetLines);

            long sumArrangements = 0;

            foreach (SpringStatusSheet3 sheet in springStatusSheets)
            {
                sumArrangements += sheet.ArrangementsCount;
            }

            return sumArrangements.ToString();
            */
            return "";
        }

        public string SolvePart2(string[] datasetLines)
        {
            /*
            SpringStatusSheet2[] springStatusSheets = BuildSpringStatusSheets2(datasetLines);
            
            long sumArrangements = 0;

            //int x = 1;
            
            foreach (SpringStatusSheet2 sheet in springStatusSheets)
            {
                sumArrangements += sheet.ArrangementsCount;
                //x++;
            }

            return sumArrangements.ToString();
            */
            return "";
        }

        /*
        private SpringStatusSheet3[] BuildSpringStatusSheets(string[] datasetLines)
        {
            int simultanousTasks = 3;
            SpringStatusSheet3[] result = Array.Empty<SpringStatusSheet3>();

            int tasksSize = (int)Math.Ceiling((double)datasetLines.Length / simultanousTasks);
            Task<SpringStatusSheet3[]>[] tasks = new Task<SpringStatusSheet3[]>[tasksSize];

            for (int tasksIndex = 0; tasksIndex < tasksSize; tasksIndex++)
            {
                int maxLineIndex = tasksSize * (tasksIndex + 1);
                maxLineIndex = datasetLines.Length < maxLineIndex ? datasetLines.Length : maxLineIndex;

                tasks[tasksIndex] = Task.Run(() =>
                {
                    int minLineIndex = tasksSize * tasksIndex;
                    SpringStatusSheet3[] taskResult = new SpringStatusSheet3[maxLineIndex - minLineIndex];
                    int taskResultIndex = 0;

                    for (int datasetLinesIndex = minLineIndex; datasetLinesIndex < maxLineIndex; datasetLinesIndex++)
                    {
                        string[] data = datasetLines[datasetLinesIndex].Split(' ');
                        taskResult[taskResultIndex] = new SpringStatusSheet3(data[0], data[1]);

                        taskResultIndex++;
                    }
                    return taskResult;
                });

            }

            Task.WaitAll(tasks);

            int resultLength = 0;
            foreach (var task in tasks) 
            {
                resultLength += task.Result.Length;
            }

            result = new SpringStatusSheet3[resultLength];
            int resultIndex = 0;

            foreach (var task in tasks)
            {
                for (int i = 0; i < task.Result.Length; i++, resultIndex++)
                {
                    result[resultIndex] = task.Result[i];
                }
            }

            return result;
        }
        */

        /*
        private SpringStatusSheet2[] BuildSpringStatusSheets2(string[] datasetLines)
        {
            SpringStatusSheet2[] result = new SpringStatusSheet2[datasetLines.Length];

            for (int i = 0; i < datasetLines.Length; i++)
            {
                string[] data = datasetLines[i].Split(' ');
                Memory3[] damagedGroups = UnfoldFives(data[1], ',').Split(',').Select(a => new Memory3(int.Parse(a))).ToArray();

                result[i] = (new SpringStatusSheet2(UnfoldFives(data[0], '?'), damagedGroups));
            }

            return result;
        }
        */
        private string UnfoldFives(string toRepeat, char separator)
        {
            return String.Join(separator, Enumerable.Repeat(toRepeat, 5));
        }

    }
}
