using AdventOfCode2024Solutions.Day04;
using Common;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode2024Solutions.Day08
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 8: ";

        protected string[] Grid = Array.Empty<string>();

        public string SolvePart1(string[] datasetLines)
        {
            Grid = datasetLines;

            BuildAntennaPairs(out Dictionary<char, Dictionary<Vector2I, List<Vector2I>>> antennaPairs);

            BuildHotSpots(antennaPairs, out List<Vector2I> hotSpots);

            return hotSpots.Count.ToString();
        }

        protected void BuildAntennaPairs(out Dictionary<char, Dictionary<Vector2I, List<Vector2I>>> antennaPairs)
        {
            antennaPairs = new Dictionary<char, Dictionary<Vector2I, List<Vector2I>>>();

            for (int y = 0; y < Grid.Length; y++)
            {
                string row = Grid[y];
                for (int x = 0; x < row.Length; x++)
                {
                    char c = row[x];
                    if ('.' != row[x])
                    {
                        if (antennaPairs.TryGetValue(c, out Dictionary<Vector2I, List<Vector2I>>? pairs))
                        {
                            foreach (var pair in pairs)
                            {
                                pair.Value.Add(new Vector2I(x,y));
                            }
                        }
                        else
                        {
                            antennaPairs[c] = new Dictionary<Vector2I, List<Vector2I>>();
                        }

                        antennaPairs[c].Add(new Vector2I(x,y), new List<Vector2I>());

                    }
                }
            }
        }

        protected void BuildHotSpots(Dictionary<char, Dictionary<Vector2I, List<Vector2I>>> antennaPairs, out List<Vector2I> hotSpots, bool use2 = false)
        {
            hotSpots = new List<Vector2I>();
            
            foreach (var frequencies in antennaPairs)
            {
                foreach (var pairs in frequencies.Value)
                {
                    foreach (var paired in pairs.Value)
                    {
                        Vector2I?[] spots = Array.Empty<Vector2I?>();

                        if (use2)
                        {
                            GetHotSpots2(pairs.Key, paired, out spots);
                        }
                        else
                        {
                            GetHotSpots(pairs.Key, paired, out spots);
                        }
                        
                        foreach (var spot in spots)
                        {
                            if (null != spot && !hotSpots.Contains((Vector2I)spot))
                            {
                                hotSpots.Add((Vector2I)spot);
                            }
                        }
                    }
                }
            }
        }

        protected void GetHotSpots(Vector2I antennaA, Vector2I antennaB, out Vector2I?[] spots)
        {
            spots = new Vector2I?[2];
            Vector2I delta = antennaB - antennaA;

            spots[0] = antennaA - delta;
            spots[1] = antennaB + delta;

            for(int i = 0; i < spots.Length; i++)
            {
                if (((Vector2I)spots[i]).X < 0
                    || ((Vector2I)spots[i]).Y < 0
                    || Grid.First().Length <= ((Vector2I)spots[i]).X
                    || Grid.Length <= ((Vector2I)spots[i]).Y )
                {
                    spots[i] = null;
                }
            }
            
        }

        public string SolvePart2(string[] datasetLines)
        {
            Grid = datasetLines;

            BuildAntennaPairs(out Dictionary<char, Dictionary<Vector2I, List<Vector2I>>> antennaPairs);

            BuildHotSpots(antennaPairs, out List<Vector2I> hotSpots, true);

            return hotSpots.Count.ToString();
        }

        protected void GetHotSpots2(Vector2I antennaA, Vector2I antennaB, out Vector2I?[] spots)
        {
            List<Vector2I> spotsList = [];
            Vector2I delta = antennaB - antennaA;
            
            Vector2I spot = antennaA;
            
            while (0 <= spot.X
                && 0 <= spot.Y
                && spot.X < Grid.First().Length
                && spot.Y < Grid.Length)
            {
                spotsList.Add(spot);

                spot -= delta;
            }

            spot = antennaB;
            
            while (0 <= spot.X
                && 0 <= spot.Y
                && spot.X < Grid.First().Length
                && spot.Y < Grid.Length)
            {
                spotsList.Add(spot);

                spot += delta;
            }
            
            spots = spotsList.Select(a=> (Vector2I?)a ).ToArray();
        }

    }
}
