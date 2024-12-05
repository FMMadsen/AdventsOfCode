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
        protected string DocumentValue { get; set; }
        public string Document {  get { return DocumentValue; } }
        protected Int16[] LevelsValue { get; set; }
        public Int16[] Levels { get { return LevelsValue; } }
        protected DirectionPosition[] DirectionsUp { get; set; } = Array.Empty<DirectionPosition>();
        protected DirectionPosition[] DirectionsDown { get; set; } = Array.Empty<DirectionPosition>();
        protected DirectionPosition[] DirectionsFlat { get; set; } = Array.Empty<DirectionPosition>();
        public DirectionPosition[] DirectionsJump { get; set; } = Array.Empty<DirectionPosition>();
        public int MaxDirection {  get; set; }
        protected int DampenersValue { get; set; } = 0;
        public int Dampeners { get { return DampenersValue; } }

        public Report(string reportText, bool useDampener=false) 
        {
            DocumentValue = reportText;
            LevelsValue = Report.GetLevelsFromText(reportText);
            MaxDirection = 0;
            if (useDampener) { DampenersValue++; }
            SetDirections();
            Analyse();
        }

        private static Int16[] GetLevelsFromText(string reportText)
        {
            return reportText.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(a => Int16.Parse(a)).ToArray();
        }

        protected void SetDirections()
        {
            MaxDirection = 0;
            var dUp = Enumerable.Empty<DirectionPosition>();
            var dDown = Enumerable.Empty<DirectionPosition>();
            var dFlat = Enumerable.Empty<DirectionPosition>();
            var dJump = Enumerable.Empty<DirectionPosition>();

            for (int nextDirection, i = 1; i < Levels.Length; i++)
            {
                nextDirection = Levels[i] - Levels[i-1];
                
                if (0 < nextDirection)
                {
                    dUp = dUp.Append(new DirectionPosition() { levelIndex = i, deltaLevel = nextDirection });
                    if (MaxDirection < nextDirection)
                    {
                        MaxDirection = nextDirection;
                    }

                    if (3 < nextDirection)
                    {
                        dJump = dJump.Append(new DirectionPosition() { levelIndex = i, deltaLevel = nextDirection });
                    }
                }
                else if (nextDirection < 0)
                {
                    dDown = dDown.Append(new DirectionPosition() { levelIndex = i, deltaLevel = nextDirection });
                }
                else
                {
                    dFlat = dFlat.Append(new DirectionPosition() { levelIndex = i, deltaLevel = nextDirection });
                }
            }

            // Turn down to up
            if (dUp.Count() < dDown.Count())
            {
                LevelsValue = Levels.Reverse().ToArray();
                SetDirections();
            }
            else
            {
                DirectionsJump = dJump.ToArray();
                DirectionsUp = dUp.ToArray();
                DirectionsDown = dDown.ToArray();
                DirectionsFlat = dFlat.ToArray();
            }

        }

        protected void Analyse()
        {
            while (0 < DampenersValue 
                &&(0 < DirectionsFlat.Length 
                || 0 < DirectionsDown.Length 
                || 0 < DirectionsJump.Length))
            {
                int remove = -1;

                if (0 < DirectionsFlat.Length)
                {
                    remove = DirectionsFlat[0].levelIndex;
                }
                else if (0 < DirectionsDown.Length)
                {
                    if (DoesRemoveBeforeHelp(DirectionsDown[0].levelIndex))
                    {
                        remove = DirectionsDown[0].levelIndex - 1; 
                    }
                    else
                    {
                        remove = DirectionsDown[0].levelIndex;
                    }
                }
                else if (0 < DirectionsJump.Length)
                {
                    if (1 == DirectionsJump[0].levelIndex )
                    {
                        remove = 0;
                    }
                    else if (DirectionsJump[0].levelIndex == Levels.Length - 1)
                    {
                        remove = DirectionsJump[0].levelIndex;
                    }
                    else
                    {
                        DampenersValue--;
                    }
                }

                if (-1 < remove)
                {
                    LevelsValue = Levels.Where((a, i) => i != remove).ToArray();
                    SetDirections();
                    DampenersValue--;
                }
            }

            if (0 < DirectionsDown.Length + DirectionsFlat.Length + DirectionsJump.Length)
            {
                MaxDirection = 0;
            }

        }

        protected bool DoesRemoveBeforeHelp(int levelIndex)
        {
            if (levelIndex < 1) { return false; }

            var before = levelIndex - 1;
            var after = levelIndex + 1;

            bool doesHelp = true;

            if (0 < before)
            {
                if (Levels[levelIndex] <= Levels[before - 1]
                    || 3 < Levels[levelIndex] - Levels[before - 1])
                {
                    doesHelp = false;
                }
            }

            if (after < Levels.Length)
            {
                if (Levels[after] <= Levels[levelIndex]
                    || 3 < Levels[after] - Levels[levelIndex])
                {
                    doesHelp = false;
                }
            }

            return doesHelp;
        }
    }
}
