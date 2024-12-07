using Common;

namespace AdventOfCode2024Solutions.Day05
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 5: ";

        public string SolvePart1(string[] datasetLines)
        {
            int total = 0;

            ConvertDataset(datasetLines, out Page[][] updates);

            foreach (Page[] update in updates)
            {
                if (SortUpdate(update, out Page[] newOrder))
                {
                    total += newOrder[newOrder.Length / 2].PageNumber;
                }
            }

            return total.ToString();
        }

        protected bool SortUpdate(Page[] update, out Page[] newOrder)
        {
            bool inOrder = true;
            newOrder = new Page[update.Length];

            for (int updateI = update.Length - 1; 0 <= updateI; updateI--)
            {
                int current = PlaceInLastEmpty(newOrder, update[updateI]);
                if (-1 == current)
                {
                    throw new IndexOutOfRangeException("There must be room in newOrder for all Pages");
                }

                int[] dependingPagesIndexes = update.Skip(updateI + 1).Where(a => update[updateI].Dependencies.Contains(a.PageNumber)).Select((a, i) => i).ToArray();

                if (0 < dependingPagesIndexes.Length)
                {
                    inOrder = false;

                    /*

                    foreach (int dependingPageIndex in dependingPagesIndexes)
                    {
                        PlaceInLastEmpty(newOrder, update[dependingPageIndex]);

                        for (int tempI = dependingPageIndex; tempI <= current; tempI--)
                        {
                            int place = updateI - 1;
                            if ()
                            {

                            }

                        }
                    }
                    */
                }
                else
                {

                }

            }

            return inOrder;
        }

        protected int PlaceInLastEmpty(Page[] aList, Page aPage)
        {
            for (int i = aList.Length - 1; 0 <= i; i--)
            {
                if(null == aList[i])
                {
                    aList[i] = aPage;
                    return i;
                }
                else if(aPage == aList[i])
                {
                    return i;
                }
            }

            return -1;
        }

        protected Page[] ToPageList(string input, Dictionary<int, int[]> rules)
        {
            return input
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(a=> new Page(Int32.Parse(a), 
                    rules.TryGetValue(Int32.Parse(a), out int[] dependencies) ? dependencies : Array.Empty<int>()
                    ) )
                .ToArray();
        }

        protected void ConvertDataset(string[] datasetLines, out Page[][] updates)
        {
            Dictionary<int, int[]> rules = new Dictionary<int, int[]>();

            int i = 0;

            while ("" != datasetLines[i])
            {
                string[] ruleTemp = datasetLines[i].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                int page = Int32.Parse(ruleTemp[1]);

                if (rules.TryGetValue(page, out int[] rulesPage))
                {
                    rules[page] = [.. rulesPage, Int32.Parse(ruleTemp[0])];
                }
                else
                {
                    rules.Add(page, [Int32.Parse(ruleTemp[0])] );
                }

                i++;
            }

            i++;

            updates = new Page[datasetLines.Length - i][];
            int updatesI = 0;

            while (i < datasetLines.Length)
            {
                updates[updatesI] = ToPageList(datasetLines[i], rules);


                updatesI++;
                i++;
            }
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
