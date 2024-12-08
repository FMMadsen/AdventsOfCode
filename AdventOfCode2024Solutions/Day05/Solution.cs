using Common;
using static System.Net.Mime.MediaTypeNames;

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
                    int index = newOrder.Length / 2;
                    total +=  newOrder[index].PageNumber;
                }
            }

            return total.ToString();
        }

        protected static bool SortUpdate(Page[] update, out Page[] newOrder)
        {
            bool inOrder = true;

            for (int updateI = 0; updateI < update.Length; updateI++)
            {
                int[] dependingOnPages = UnorderedDependencies(update, updateI);

                if (0 < dependingOnPages.Length)
                {
                    inOrder = false;
                    break;
                }

            }

            if (!inOrder)
            {
                MoveDependencies(update, out newOrder);
            }
            else
            {
                newOrder = update;
            }

            return inOrder;
        }

        protected static int[] UnorderedDependencies(Page[] pages, int current)
        {
            return pages.Skip(current + 1)
                    .Where(a => pages[current].Dependencies.Contains(a.PageNumber))
                    .Select((a, i) => i)
                    .ToArray();
        }

        protected static int PlaceInLastEmpty(Page?[] aList, Page aPage)
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

        protected static Page[] ToPageList(string input, Dictionary<int, int[]> rules)
        {
            return input
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(a=> new Page(Int32.Parse(a), 
                    rules.TryGetValue(Int32.Parse(a), out int[] dependencies) ? dependencies : Array.Empty<int>()
                    ) )
                .ToArray();
        }

        protected static void ConvertDataset(string[] datasetLines, out Page[][] updates)
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
            int total = 0;

            ConvertDataset(datasetLines, out Page[][] updates);

            foreach (Page[] update in updates)
            {
                if (!SortUpdate(update, out Page[] newOrder))
                {
                    int index = newOrder.Length / 2;
                    total += newOrder[index].PageNumber;
                }
            }

            return total.ToString();
        }

        protected static void MoveDependencies(Page[] pages, out Page[] newOrder)
        {
            var buildOrder = Enumerable.Empty<Page>();

            for (int i = 0; i < pages.Length; i++)
            {
                buildOrder = AddPage(pages, buildOrder, pages[i]);
            }

            newOrder = buildOrder.ToArray();
        }

        protected static IEnumerable<Page> AddPage(Page[] pages, IEnumerable<Page> newOrder, Page aPage)
        {
            if (newOrder.Contains(aPage))
            {
                return newOrder;
            }

            var dependencies = pages.Where(a=> aPage.Dependencies.Contains(a.PageNumber) );

            foreach(Page dependency in dependencies) 
            {
                newOrder = AddPage(pages, newOrder, dependency);
            }

            if (newOrder.Contains(aPage))
            {
                return newOrder;
            }
            
            return newOrder.Append(aPage);
        }
    }
}
