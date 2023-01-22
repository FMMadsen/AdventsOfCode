namespace AdventsOfCode2022.Day06TuningTrouble
{
    /// <summary>
    /// Time consumption PART 1: 18:00 - 18:55 = <1h
    /// Time consumption PART 2: 23:00 - 23:05 = 5m
    /// Time consumption TOTAL: 1h
    /// </summary>
    internal class Day6Puzzle
    {
        internal static string SolvePart1(string[] datasetLines, bool doPrintOut)
        {
            var results = new List<string>();

            foreach(var dataBuffer in datasetLines)
            {
                var subroutine = new SubroutineStartOfPacket(4);
                if(subroutine.LoadBuffer(dataBuffer))
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

        internal static string SolvePart2(string[] datasetLines, bool doPrintOut)
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
