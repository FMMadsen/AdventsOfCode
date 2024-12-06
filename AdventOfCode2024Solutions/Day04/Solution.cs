using Common;
using System.Numerics;

namespace AdventOfCode2024Solutions.Day04
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 4: ";
        public string SearchWord = "XMAS";

        private int ShapeXIntersectIndexValue = -1;
        protected int ShapeXIntersectIndex 
        { 
            get
            {
                if (-1 < ShapeXIntersectIndexValue)
                {
                    return ShapeXIntersectIndexValue;
                }

                ShapeXIntersectIndexValue = SearchWord.Length / 2;

                return ShapeXIntersectIndexValue;
            } 
        }

        protected Vector2I Maximum = new Vector2I();
        protected string[] Matrix = Array.Empty<string>();

        public string SolvePart1(string[] datasetLines)
        {
            SearchWord = "XMAS";
            Matrix = datasetLines;
            Maximum = new Vector2I(Matrix[0].Length - 1, Matrix.Length - 1);

            int count = 0;

            int lineNumber = 0;
            int lineIndex = 0;

            while (lineNumber < Matrix.Length) 
            {
                while (lineIndex < Matrix[0].Length)
                {
                    count += CheckForWord(new Vector2I(lineIndex, lineNumber));

                    lineIndex++;
                }

                lineIndex = 0;
                lineNumber++;
            }

            return count.ToString();
        }

        protected int CheckForWord(Vector2I position)
        {
            int count = 0;
            int wordIndex = 0;

            if (1 < SearchWord.Length && IsCharInPosition(SearchWord[wordIndex], position))
            {
                wordIndex++;

                bool canGoNorth = SearchWord.Length -1 <= position.Y;
                bool canGoEast = position.X <= Maximum.X - (SearchWord.Length - 1);
                bool canGoSouth = position.Y <= Maximum.Y - (SearchWord.Length - 1);
                bool canGoWest = SearchWord.Length - 1 <= position.X;

                if (canGoNorth) 
                {
                    if (CheckDirection(Vector2I.NN, wordIndex, position))
                    {
                        count++;
                    }

                    if (canGoWest)
                    {
                        if (CheckDirection(Vector2I.NW, wordIndex, position))
                        {
                            count++;
                        }
                    }

                    if (canGoEast)
                    {
                        if (CheckDirection(Vector2I.NE, wordIndex, position))
                        {
                            count++;
                        }
                    }
                }

                if (canGoSouth)
                {
                    if (CheckDirection(Vector2I.SS, wordIndex, position))
                    {
                        count++;
                    }

                    if (canGoWest)
                    {
                        if (CheckDirection(Vector2I.SW, wordIndex, position))
                        {
                            count++;
                        }
                    }

                    if (canGoEast)
                    {
                        if (CheckDirection(Vector2I.SE, wordIndex, position))
                        {
                            count++;
                        }
                    }
                }

                if (canGoWest)
                {
                    if (CheckDirection(Vector2I.WW, wordIndex, position))
                    {
                        count++;
                    }
                }

                if (canGoEast)
                {
                    if (CheckDirection(Vector2I.EE, wordIndex, position))
                    {
                        count++;
                    }
                }
            }
            else if(IsCharInPosition(SearchWord[wordIndex], position))
            {
                count++;
            }
                
            return count;
        }

        protected bool CheckDirection(Vector2I direction, int wordIndex, Vector2I startPosition)
        {
            bool success = false;
            Vector2I distance = wordIndex * direction;
            Vector2I searchPos = startPosition + (wordIndex * direction);

            if (IsCharInPosition(SearchWord[wordIndex], startPosition + (wordIndex * direction)))
            {
                if (wordIndex + 1 < SearchWord.Length)
                {
                    success = CheckDirection(direction, wordIndex + 1, startPosition);
                }
                else
                {
                    success = true;
                }
            }

            return success;
        }

        protected bool IsCharInPosition(char aChar, Vector2I index)
        {
            return Matrix[index.Y][index.X] == aChar;
        }


        public string SolvePart2(string[] datasetLines)
        {
            SearchWord = "MAS";
            Matrix = datasetLines;
            Maximum = new Vector2I(Matrix[0].Length - 1, Matrix.Length - 1);

            int count = 0;

            int lineNumber = 0;
            int lineIndex = 0;

            while (lineNumber < Matrix.Length)
            {
                while (lineIndex < Matrix[0].Length)
                {
                    count += 2 == CheckForWordShapedX(new Vector2I(lineIndex, lineNumber)) ? 1 : 0;

                    lineIndex++;
                }

                lineIndex = 0;
                lineNumber++;
            }

            return count.ToString();
        }

        protected int CheckForWordShapedX(Vector2I position)
        {
            int count = 0;

            if (1 < SearchWord.Length && IsCharInPosition(SearchWord[ShapeXIntersectIndex], position))
            {

                bool canGoNorth = 0 <= position.Y - (SearchWord.Length - 1 - ShapeXIntersectIndex)
                               && position.Y + ShapeXIntersectIndex <= Maximum.Y;

                bool canGoEast =  position.X + (SearchWord.Length - 1 - ShapeXIntersectIndex) <= Maximum.X
                               && 0 <= position.X - ShapeXIntersectIndex;

                bool canGoSouth = position.Y + (SearchWord.Length - 1 - ShapeXIntersectIndex) <= Maximum.Y
                               && 0 <= position.Y - ShapeXIntersectIndex;

                bool canGoWest = 0 <= position.X - (SearchWord.Length - 1 - ShapeXIntersectIndex)
                               && position.X + ShapeXIntersectIndex <= Maximum.X;

                if (canGoNorth)
                {
                    if (canGoWest)
                    {
                        if (CheckDirection(Vector2I.NW, 0, position + (Vector2I.SE * ShapeXIntersectIndex)))
                        {
                            count++;
                        }
                    }

                    if (canGoEast)
                    {
                        if (CheckDirection(Vector2I.NE, 0, position + (Vector2I.SW * ShapeXIntersectIndex)))
                        {
                            count++;
                        }
                    }
                }

                if (canGoSouth)
                {
                    if (canGoWest)
                    {
                        if (CheckDirection(Vector2I.SW, 0, position + (Vector2I.NE * ShapeXIntersectIndex)))
                        {
                            count++;
                        }
                    }

                    if (canGoEast)
                    {
                        if (CheckDirection(Vector2I.SE, 0, position + (Vector2I.NW * ShapeXIntersectIndex)))
                        {
                            count++;
                        }
                    }
                }
            }
            else if (IsCharInPosition(SearchWord[ShapeXIntersectIndex], position))
            {
                count++;
            }

            return count;
        }
    }
}
