using AdventOfCode2024Solutions.Day04;
using Common;
using System;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2024Solutions.Day12
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 12: ";

        Region[] Regions { get; set; } = Array.Empty<Region>();

        public string SolvePart1(string[] datasetLines)
        {
            if (!Load(datasetLines))
            {
                return "";
            }

            int price = 0;

            foreach (Region region in Regions)
            {
                price += region.FenceDistance * region.Area.Length;
            }

            return price.ToString();
        }

        protected bool Load(string[] datasetLines)
        {
            if (datasetLines.Length < 1) { return false; }

            Regions = Array.Empty<Region>();

            for (int y = 0; y < datasetLines.Length; y++)
            {
                for (int x = 0; x < datasetLines.First().Length; x++)
                {
                    AddCrop(datasetLines, new Vector2I(x, y));
                }
            }

            return true;
        }

        protected Region? GetRegion(string[] matrix, Vector2I index, char symbol)
        {
            Region? currentRegion = null;
            char currentSymbol = CharOf(matrix, index);

            if (symbol == currentSymbol)
            {
                Region[] regions = Regions.Where((a, i) => a.Id == symbol && a.Area.Contains(index)).ToArray();
                if(0 < regions.Length)
                {
                    currentRegion = regions.First();
                }
            }

            return currentRegion;
        }

        protected void AddCrop(string[] matrix, Vector2I index)
        {
            IEnumerable<Fence> fences = Enumerable.Empty<Fence>();
            char currentChar = CharOf(matrix, index);
            Region? currentRegion = null;
            
            Fence? currentFence;

            // check north
            if (0 < index.Y)
            {
                CheckDirection(matrix, index, Vector2I.NN, currentRegion, currentChar, out currentRegion, fences, out fences, true);
            }
            else
            {
                currentFence = new Fence(){ Direction = Vector2I.EE };
                currentFence.BelongsToIndex = currentFence.BelongsToIndex.Append(index).ToArray();
                fences = fences.Append(currentFence);
            }

            // check east
            if (index.X < matrix.First().Length-1)
            {
                CheckDirection(matrix, index, Vector2I.EE, currentRegion, currentChar, out currentRegion, fences, out fences, false);
            }
            else
            {
                currentFence = new Fence() { Direction = Vector2I.SS };
                currentFence.BelongsToIndex = currentFence.BelongsToIndex.Append(index).ToArray();
                fences = fences.Append(currentFence);
            }

            // check south
            if (index.Y < matrix.Length-1)
            {
                CheckDirection(matrix, index, Vector2I.SS, currentRegion, currentChar, out currentRegion, fences, out fences, false);
            }
            else
            {
                currentFence = new Fence() { Direction = Vector2I.WW };
                currentFence.BelongsToIndex = currentFence.BelongsToIndex.Append(index).ToArray();
                fences = fences.Append(currentFence);
            }

            // check west
            if (0 < index.X)
            {
                CheckDirection(matrix, index, Vector2I.WW, currentRegion, currentChar, out currentRegion, fences, out fences, true);
            }
            else
            {
                currentFence = new Fence() { Direction = Vector2I.NN };
                currentFence.BelongsToIndex = currentFence.BelongsToIndex.Append(index).ToArray();
                fences = fences.Append(currentFence);
            }

            if (null == currentRegion)
            {
                currentRegion = new Region(currentChar);
                Regions = Regions.Append(currentRegion).ToArray();
            }

            currentRegion.Area = currentRegion.Area.Append(index).ToArray();
            MergeFences(fences, currentRegion);
        }

        protected void CheckDirection(string[] matrix, Vector2I fromIndex, Vector2I direction, Region? currentRegion, char currentChar, out Region? resultRegion, IEnumerable<Fence> fences, out IEnumerable<Fence> newFences, bool before = false)
        {
            resultRegion = currentRegion;
            newFences = fences;
            Vector2I directionIndex = fromIndex + direction;
            
            Fence? currentFence = null;

            if (null == currentRegion && before)
            {
                resultRegion = GetRegion(matrix, directionIndex, currentChar);

                if (null == resultRegion)
                {
                    currentFence = new Fence() { Direction = direction.TurnRight() };
                    currentFence.BelongsToIndex = currentFence.BelongsToIndex.Append(fromIndex).ToArray();
                    newFences = newFences.Append(currentFence);
                }
            }
            else if (null != resultRegion && before)
            {
                Region? previous = GetRegion(matrix, directionIndex, currentChar);
                if (null != previous && previous != currentRegion)
                {
                    resultRegion.Area = resultRegion.Area.Union(previous.Area).ToArray();
                    MergeFences(previous.Fences, resultRegion);

                    previous.Area = Array.Empty<Vector2I>();
                    previous.Fences = Array.Empty<Fence>();
                }
                else if(null == previous)
                {
                    currentFence = new Fence() { Direction = direction.TurnRight() };
                    currentFence.BelongsToIndex = currentFence.BelongsToIndex.Append(fromIndex).ToArray();
                    newFences = newFences.Append(currentFence);
                }
            }
            else if (CharOf(matrix, directionIndex) != currentChar)
            {
                currentFence = new Fence() { Direction = direction.TurnRight() };
                currentFence.BelongsToIndex = currentFence.BelongsToIndex.Append(fromIndex).ToArray();
                newFences = newFences.Append(currentFence);
            }

        }

        public static char CharOf(string[] matrix, Vector2I index)
        {
            return matrix[index.Y][index.X];
        }

        public string SolvePart2(string[] datasetLines)
        {
            if (!Load(datasetLines))
            {
                return "";
            }

            int price = 0;

            Regions = Regions.Where(a=> 0 < a.Area.Length).ToArray();
            
            foreach (Region region in Regions)
            {
                price += region.Fences.Length * region.Area.Length;
            }
            
            return price.ToString();
        }

        protected void MergeFences(IEnumerable<Fence> fences, Region currentRegion)
        {
            foreach (Fence aFence in fences)
            {
                Vector2I[] aBelongedIndexTemp = aFence.BelongsToIndex;
                if (0 < aBelongedIndexTemp.Length)
                {
                    Vector2I aBelongedIndex = aBelongedIndexTemp.First();

                    bool isNeighbor = false;

                    foreach (Fence bFence in currentRegion.Fences)
                    {
                        if (bFence.Direction == aFence.Direction)
                        {
                            foreach (Vector2I bBelongedIndex in bFence.BelongsToIndex)
                            {
                                if (bBelongedIndex == aBelongedIndex - aFence.Direction || bBelongedIndex == aBelongedIndex + aFence.Direction)
                                {
                                    isNeighbor = true; break;
                                }
                            }

                            if (isNeighbor)
                            {
                                bFence.BelongsToIndex = bFence.BelongsToIndex.Append(aBelongedIndex).ToArray();
                                break;
                            }
                        }
                        
                    }

                    if (!isNeighbor)
                    {
                        currentRegion.Fences = currentRegion.Fences.Append(aFence).ToArray();
                    }
                }
                


                    

                
            }
        }


    }
}
