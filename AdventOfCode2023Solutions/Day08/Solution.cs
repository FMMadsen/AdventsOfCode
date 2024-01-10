using Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2023Solutions.Day08
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 8: Haunted Wasteland";
        string[] DatasetLines;

        public readonly Directions Directions;

        public readonly Dictionary<string, string[]> Map = new Dictionary<string, string[]>();

        public void Initialize(string[] datasetLines)
        {
            DatasetLines = datasetLines;

            Directions = new Directions(DatasetLines[0].Trim());

            Regex pattern = new Regex(@"\w\w\w");

            int i = 2;
            while(i < DatasetLines.Length)
            {
                MatchCollection match = pattern.Matches(DatasetLines[i]);
                Map.Add(match[0].Value, [match[1].Value, match[2].Value]);

                i++;
            }
        }

        public string SolvePart1(string[] datasetLines)
        {
            Initialize(string[] datasetLines)

            int attempts = 0;
            string key = "AAA";
            Directions.DirectionIndex = 0;
            while ("ZZZ" != key)
            {
                attempts++;
                key = Map[key][Directions.DirectionIterate];
            }
            return attempts.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            Initialize(string[] datasetLines)

            string[] keys = Map.Keys.ToArray().Where(a => a[2] == 'A').ToArray();
            int[] attemptsAtoZ = new int[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                Directions.DirectionIndex = 0;

                while ('Z' != keys[i][2])
                {
                    attemptsAtoZ[i]++;
                    keys[i] = Map[keys[i]][Directions.DirectionIterate];
                }

            }

            attemptsAtoZ.Order();

            int[] primes = CreatePrimesUntil(attemptsAtoZ[0]);
            int[] newAttempts = attemptsAtoZ.ToArray();
            List<int> refactores = new List<int>();


            for (int p = 0; p < primes.Length; p++)
            {
                if (primes[p] >= attemptsAtoZ[0])
                {
                    break;
                }
                bool devisionSuccess = true;
                for (int i = 0; i < attemptsAtoZ.Length; i++)
                {
                    newAttempts[i] = attemptsAtoZ[i] / primes[p];
                    if (newAttempts[i] * 10 != attemptsAtoZ[i] * 10 / primes[p])
                    {
                        devisionSuccess = false;
                        break;
                    }
                }
                if (devisionSuccess)
                {
                    attemptsAtoZ = newAttempts.ToArray();
                    refactores.Add(primes[p]);
                    p = -1;
                }
                
            }

            long result = 1;

            foreach (long t in attemptsAtoZ)
            {
                result *= t;
            }
            foreach (long t in refactores)
            {
                result *= t;
            }

            return result.ToString();
        }

        public int[] CreatePrimesUntil(int max)
        {
            List<int> primes = new List<int>() {2};
            //PSquare = (int)Math.Sqrt(max) + 1;

            for (int i = 3; i <= max; i=i+2)
            {
                bool isPrime = true;
                int PSquare = (int)Math.Sqrt(i) + 1;
                for (int p = 0; p < primes.Count; p++)
                {
                    if (i % primes[p] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    if (primes[p] >= PSquare) { break; }
                }
                if (isPrime)
                {
                    primes.Add(i);
                }

                
            }

            return primes.ToArray();
        }

        public string SolvePart2IterationCheck() 
        {
            string[] keys = Map.Keys.ToArray().Where(a => a[2] == 'A').ToArray();
            long[] attemptsAtoZ = new long[keys.Length];
            long[] attemptsZtoZ = new long[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                Directions.DirectionIndex = 0;
                while ('Z' != keys[i][2])
                {
                    attemptsAtoZ[i]++;
                    keys[i] = Map[keys[i]][Directions.DirectionIterate];
                }

                attemptsZtoZ[i]++;
                keys[i] = Map[keys[i]][Directions.DirectionIterate];

                while ('Z' != keys[i][2])
                {
                    attemptsZtoZ[i]++;
                    keys[i] = Map[keys[i]][Directions.DirectionIterate];
                }
            }

            return String.Join(' ', attemptsAtoZ) + "\n" + String.Join(' ', attemptsZtoZ);
        }

        public string SolvePart2Old()
        {
            
            
            long attempts = 0;
            string[] keys = Map.Keys.ToArray().Where(a => a[2] == 'A').ToArray();
            Directions.DirectionIndex = 0;
            int nextDirection = 0;
            while (keys.Count(a => a[2] == 'Z') != keys.Length)
            {
                attempts++;
                nextDirection = Directions.DirectionIterate;
                for (int i = 0; i < keys.Length; i++)
                {
                    keys[i] = Map[keys[i]][nextDirection];
                }
                Console.Write("\rAttempts: " + attempts.ToString());
            }
            Console.Write("\r\n");

            return attempts.ToString();
        }
    }
}
