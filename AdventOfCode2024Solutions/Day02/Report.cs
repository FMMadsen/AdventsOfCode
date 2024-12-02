using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day02
{
    public class Report
    {
        public string Document {  get; set; }
        public int[] Levels { get; set; }
        public int MaxDirection {  get; set; }

        public Report(string reportText) 
        {
            Document = reportText;
            Levels = Report.GetLevelsFromText(reportText);
            Analyse(Levels);
        }

        private static int[] GetLevelsFromText(string reportText)
        {
            return reportText.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(a => Int32.Parse(a)).ToArray();
        }

        protected void Analyse(int[] levels)
        {
            MaxDirection = 0;

            if (1 < levels.Length) 
            {
                int end = levels.Length - 1;
                MaxDirection = levels[1] - levels[0];

                if (0 < MaxDirection)
                {
                    for (int i = 1; i < end; i++)
                    {
                        int nextDirection = levels[i + 1] - levels[i];

                        if (0 < nextDirection)
                        {
                            if (MaxDirection < nextDirection)
                            {
                                MaxDirection = nextDirection;
                            }
                        }
                        else 
                        {
                            MaxDirection = 0;
                            return;
                        }
                    }
                }
                else if(0 > MaxDirection)
                {
                    for (int i = 1; i < end; i++)
                    {
                        int nextDirection = levels[i + 1] - levels[i];

                        if (0 > nextDirection)
                        {
                            if (nextDirection < MaxDirection)
                            {
                                MaxDirection = nextDirection;
                            }
                        }
                        else
                        {
                            MaxDirection = 0;
                            return;
                        }
                    }
                }

                
            }

        }
    }
}
