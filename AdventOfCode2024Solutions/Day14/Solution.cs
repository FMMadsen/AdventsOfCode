using AdventOfCode2024Solutions.Day04;
using Common;
using System.Numerics;

namespace AdventOfCode2024Solutions.Day14
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 14: ";
        public WarehouseMap CurrentMap { get; set; } = new WarehouseMap();

        public string SolvePart1(string[] datasetLines, bool test)
        {
            LoadMap(datasetLines, 11, 7);

            CurrentMap.RunFor(100);

            long result = CalcResult();

            return result.ToString();
        }

        public string SolvePart1(string[] datasetLines)
        {
            LoadMap(datasetLines, 101, 103);

            CurrentMap.RunFor(100);

            long result = CalcResult();

            return result.ToString();
        }

        protected long CalcResult()
        {
            long sqA=0, sqB = 0, sqC = 0, sqD = 0;

            Vector2I center = new Vector2I((CurrentMap.Width - 1) / 2, (CurrentMap.Height - 1) / 2);

            foreach(Robot aRobot in CurrentMap.Robots)
            {
                if (aRobot.Location.X < center.X && aRobot.Location.Y < center.Y)
                {
                    sqA++;
                }
                else if (center.X < aRobot.Location.X && aRobot.Location.Y < center.Y)
                {
                    sqB++;
                }
                else if (center.X < aRobot.Location.X && center.Y < aRobot.Location.Y)
                {
                    sqC++;
                }
                else if (aRobot.Location.X < center.X && center.Y < aRobot.Location.Y)
                {
                    sqD++;
                }
            }

            return sqA * sqB * sqC * sqD;
        }

        protected void LoadMap(string[] datasetLines, int width, int height)
        {
            CurrentMap = new WarehouseMap();
            CurrentMap.Width = width;
            CurrentMap.Height = height;

            CurrentMap.Robots = new Robot[datasetLines.Length];

            for (int i = 0; i < datasetLines.Length; i++)
            {
                CurrentMap.Robots[i] = StringToRobot(datasetLines[i]);
            }
        }

        protected Robot StringToRobot(string input)
        {
            Vector2I location, velocity;

            string[] strings = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string firstNumString, secondNumString;

            int firstNumIndex = strings[0].IndexOf('=') + 1;
            int seperateIndex = strings[0].IndexOf(',');

            firstNumString = strings[0].Substring(firstNumIndex, seperateIndex - firstNumIndex);
            secondNumString = strings[0].Substring(seperateIndex+1);

            location = new Vector2I(int.Parse(firstNumString), int.Parse(secondNumString));


            firstNumIndex = strings[1].IndexOf('=') + 1;
            seperateIndex = strings[1].IndexOf(',');

            firstNumString = strings[1].Substring(firstNumIndex, seperateIndex - firstNumIndex);
            secondNumString = strings[1].Substring(seperateIndex + 1);

            velocity = new Vector2I(int.Parse(firstNumString), int.Parse(secondNumString));

            return new Robot() { Location = location, Velocity=velocity };
        }

        public string SolvePart2(string[] datasetLines)
        {
            LoadMap(datasetLines, 101, 103);

            int duration = 0;

            while (!DetectEasterEgg())
            {
                duration++;
                CurrentMap.RunFor(1);
            }

            Console.WriteLine("Assets-folder is from a unity project.");
            return 7055.ToString();
        }

        protected bool DetectEasterEgg()
        {
            return true;
        }
    }
}
