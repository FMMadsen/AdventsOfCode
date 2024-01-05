﻿using Common;

namespace AdventOfCode2023Solutions.Day09
{
    public class HistoryData
    {
        private List<int[]> _History = new List<int[]>();
        public int Predicted 
        { 
            get 
            { 
                return _History[0][_History[0].Length-1];
            } 
        }

        public int Predated
        {
            get
            {
                return _History[0][0];
            }
        }

        public List<int[]> History { get { return _History; } }

        public HistoryData(int[] history)
        {
            _History.Add(history);
        }

    }

    public class Solution(string[] DatasetLines) : IAOCSolution
    {
        public string PuzzleName => "Day 9: ";

        public string SolvePart1()
        {
            HistoryData[] historyData = new HistoryData[DatasetLines.Length];
            for (int i = 0; i < historyData.Length; i++ )
            {
                List<int> list = DatasetLines[i].Split(' ').Select(a => int.Parse(a)).ToList();
                list.Add(0);
                historyData[i] = new HistoryData(list.ToArray());
            }
            long resultSum = 0;


            foreach(HistoryData history in historyData)
            {
                for(int i = 0;i < history.History.Count;i++)
                {
                    if (0 == history.History[i].Count(a => 0 != a)){ break; }

                    int[] histSet = new int[ history.History[i].Length - 1 ];
                    for (int j = 0; j < histSet.Length-1;j++ )
                    {
                        histSet[j] = history.History[i][j + 1] - history.History[i][j];
                    }

                    history.History.Add(histSet);
                }

                for(int i = history.History.Count-1; 0 < i; i--)
                {
                    history.History[i - 1][history.History[i].Length] = history.History[i - 1][history.History[i].Length - 1] + history.History[i][history.History[i].Length - 1];
                }

                resultSum += history.Predicted;
            }

            return resultSum.ToString();
        }

        public string SolvePart2()
        {
            HistoryData[] historyData = new HistoryData[DatasetLines.Length];
            for (int i = 0; i < historyData.Length; i++)
            {
                List<int> list = [0, .. DatasetLines[i].Split(' ').Select(a => int.Parse(a)).ToList()];
                historyData[i] = new HistoryData(list.ToArray());
            }
            long resultSum = 0;

            foreach (HistoryData history in historyData)
            {
                for (int i = 0; i < history.History.Count; i++)
                {
                    if (0 == history.History[i].Count(a => 0 != a)) { break; }

                    int[] histSet = new int[history.History[i].Length - 1];
                    for (int j = 1; j < histSet.Length; j++)
                    {
                        histSet[j] = history.History[i][j + 1] - history.History[i][j];
                    }

                    history.History.Add(histSet);
                }

                for (int i = history.History.Count - 1; 0 < i; i--)
                {
                    history.History[i - 1][0] = history.History[i-1][1] - history.History[i][0];
                }

                resultSum += history.Predated;
            }

            return resultSum.ToString();
        }
    }
}
