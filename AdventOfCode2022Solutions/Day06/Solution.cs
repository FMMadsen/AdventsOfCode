using AdventsOfCode2022.Day06TuningTrouble;
using Common;

namespace AdventOfCode2022Solutions.Day06
{
    /// <summary>
    /// Time consumption PART 1: 18:00 - 18:55 = <1h
    /// Time consumption PART 2: 23:00 - 23:05 = 5m
    /// Time consumption TOTAL: 1h
    /// </summary>
    public class Solution(string[] datasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 6: Tuning Trouble";
        public string[] DatasetLines => datasetLines;
        public bool DoPrintOut => false;


        public string SolvePart1()
        {
            var results = new List<string>();

            foreach (var dataBuffer in datasetLines)
            {
                var subroutine = new SubroutineStartOfPacket(4);
                if (subroutine.LoadBuffer(dataBuffer))
                {
                    results.Add(subroutine.StartOfPacketMarkerEnd.ToString());
                }
                else
                {
                    results.Add("[none]");
                }
            }

            return string.Join(' ', results.ToArray());
        }

        public string SolvePart2()
        {
            var results = new List<string>();

            foreach (var dataBuffer in datasetLines)
            {
                var subroutine = new SubroutineStartOfPacket(14);
                if (subroutine.LoadBuffer(dataBuffer))
                {
                    results.Add(subroutine.StartOfPacketMarkerEnd.ToString());
                }
                else
                {
                    results.Add("[none]");
                }
            }

            return string.Join(' ', results.ToArray());
        }
    }
}
