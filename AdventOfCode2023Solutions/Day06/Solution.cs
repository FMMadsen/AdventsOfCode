using Common;

namespace AdventOfCode2023Solutions.Day06
{
    public class Solution(string[] DatasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 6: Wait For It";

        public string SolvePart1()
        {
            List<RaceStats> races = BuildRaceStats(DatasetLines);
            int SpeedPerHeld = 1;
            long Options = 1;

            foreach (RaceStats race in races)
            {
                int a = -SpeedPerHeld;
                long b = SpeedPerHeld * race.Time;
                long c = -race.RecordDistance;

                var d = Math.Pow(b,2) - (4 * a * c);

                double q1t = (-b + Math.Sqrt(d)) / (2 * a);
                double q2t = (-b - Math.Sqrt(d)) / (2 * a);
                int q1 = (int)Math.Ceiling(q1t);
                int q2 = (int)Math.Floor(q2t);

                if (q1t == q1) { q1++; }
                if (q2t == q2) { q2--; }

                race.Options = q2 - q1;
                if (0 > race.Options)
                {
                    race.Options = 0;
                }
                else
                {
                    race.Options++;
                }

                Options = Options * race.Options;
            }

            return Options.ToString();
        }

        public string SolvePart2()
        {
            RaceStats race = BuildRace(DatasetLines);
            int SpeedPerHeld = 1;
            
            int a = -SpeedPerHeld;
            long b = SpeedPerHeld * race.Time;
            long c = -race.RecordDistance;

            var d = Math.Pow(b, 2) - (4 * a * c);

            double q1t = (-b + Math.Sqrt(d)) / (2 * a);
            double q2t = (-b - Math.Sqrt(d)) / (2 * a);
            int q1 = (int)Math.Ceiling(q1t);
            int q2 = (int)Math.Floor(q2t);

            if (q1t == q1) { q1++; }
            if (q2t == q2) { q2--; }

            race.Options = q2 - q1;
            if (0 > race.Options)
            {
                race.Options = 0;
            }
            else
            {
                race.Options++;
            }

            return race.Options.ToString();
        }

        public List<RaceStats> BuildRaceStats(string[] datasetLines)
        {
            List<RaceStats> races = new List<RaceStats>();

            uint[] times = datasetLines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(a => uint.Parse(a)).ToArray();
            uint[] records = datasetLines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(a => uint.Parse(a)).ToArray();

            for (int i = 0; i < times.Length; i++)
            {
                races.Add(new RaceStats() {Time = times[i], RecordDistance = records[i] });
            }

            return races;
        }

        public RaceStats BuildRace(string[] datasetLines)
        {
            string[] times = datasetLines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] records = datasetLines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long time = long.Parse( String.Concat(times) );
            long record = long.Parse( String.Concat(records) );

            RaceStats race = new RaceStats() { Time = time, RecordDistance = record };
            
            return race;
        }
    }
}
