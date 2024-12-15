namespace AdventOfCode2024Solutions.Day05
{
    public class Manual
    {
        public Dictionary<string, Rule> PageRulesDictionary { get; private set; }
        public List<string[]> UpdatesSubsetCorrect { get; private set; }
        public List<string[]> UpdatesSubsetIncorrect { get; private set; }
        public List<string[]> UpdatesSubsetIncorrectFixed { get; private set; }

        public Manual(string[] rulesAndUpdateLines)
        {
            BuildRulesAndUpdatesList(rulesAndUpdateLines, out List<string[]> updates, out Dictionary<string, Rule> pageRulesDictionary);
            this.PageRulesDictionary = pageRulesDictionary;
            var pageRulesAfterCountDictionary = this.PageRulesDictionary.ToDictionary(x => x.Key, x => x.Value.PagesAfter.Count);

            CreateListOfCorrectAndIncorrectUpdates(updates, pageRulesDictionary, out List<string[]> updatesSubsetCorrect, out List<string[]> updatesSubsetIncorrect);
            this.UpdatesSubsetCorrect = updatesSubsetCorrect;
            this.UpdatesSubsetIncorrect = updatesSubsetIncorrect;

            UpdatesSubsetIncorrectFixed = [];
        }

        public long GetSumMiddleNumbersOfCorrectOrdered()
        {
            return UpdatesSubsetCorrect.Sum(x => GetMiddleNumber(x));
        }

        public long GetSumMiddleNumbersOfIncorrectOrderedFixed()
        {
            return UpdatesSubsetIncorrectFixed.Sum(x => GetMiddleNumber(x));
        }

        private void BuildRulesAndUpdatesList(
            string[] rulesAndUpdateLines,
            out List<string[]> updates,
            out Dictionary<string, Rule> pageRulesDictionary)
        {
            updates = [];
            pageRulesDictionary = [];

            var isRules = true;
            foreach (var line in rulesAndUpdateLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isRules = false;
                    continue;
                }

                if (isRules)
                    AddPageToPageRuleDictionary(line, ref pageRulesDictionary);
                else
                    updates.Add(line.Split(",", StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private void AddPageToPageRuleDictionary(string updateLine, ref Dictionary<string, Rule> pageRulesDictionary)
        {
            var currentRuleSplit = updateLine.Split("|");
            var pageLeft = currentRuleSplit[0];
            var pageRight = currentRuleSplit[1];

            if (pageRulesDictionary.ContainsKey(pageLeft))
            {
                var ruleFromindex = pageRulesDictionary[pageLeft];
                ruleFromindex.PagesAfter.Add(pageRight);
            }
            else
            {
                var newRule = new Rule(pageLeft);
                newRule.PagesAfter.Add(pageRight);
                pageRulesDictionary.Add(newRule.CurrentPage, newRule);
            }

            if (pageRulesDictionary.ContainsKey(pageRight))
            {
                var ruleToIndex = pageRulesDictionary[pageRight];
                ruleToIndex.PagesBefore.Add(pageLeft);
            }
            else
            {
                var newRule = new Rule(pageRight);
                newRule.PagesBefore.Add(pageLeft);
                pageRulesDictionary.Add(newRule.CurrentPage, newRule);
            }
        }

        private static void CreateListOfCorrectAndIncorrectUpdates(
            List<string[]> updates,
            Dictionary<string, Rule> pageRulesDictionary,
            out List<string[]> updatesSubsetCorrect,
            out List<string[]> updatesSubsetIncorrect)
        {
            updatesSubsetCorrect = [];
            updatesSubsetIncorrect = [];

            foreach (var update in updates)
            {
                var updateIsCorrect = true;
                for (var iLeft = 0; iLeft < update.Length - 1; iLeft++)
                {
                    var currentPage = update[iLeft];

                    for (var iRight = iLeft + 1; iRight < update.Length; iRight++)
                    {
                        var updatePageLeft = update[iLeft];
                        var updatePageRight = update[iRight];

                        if (pageRulesDictionary.ContainsKey(updatePageRight) && pageRulesDictionary[updatePageRight].PagesAfter.Contains(updatePageLeft))
                            updateIsCorrect = false;

                        if (!updateIsCorrect)
                            break;
                    }

                    if (!updateIsCorrect)
                        break;
                }

                if (updateIsCorrect)
                    updatesSubsetCorrect.Add(update);
                else
                    updatesSubsetIncorrect.Add(update);
            }
        }

        public void FixIncorrectUpdates()
        {
            this.UpdatesSubsetIncorrectFixed = UpdatesSubsetIncorrect.Select(x => FixIncorrectUpdate(x)).ToList();
        }

        public string[] FixIncorrectUpdate(string[] pagesUnordered)
        {
            List<string> pagesCorrectOrdered = [];

            foreach (var page in pagesUnordered)
            {
                var pageNewIndex = -1;
                for (int i = 0; i < pagesCorrectOrdered.Count; i++)
                {
                    if (this.PageRulesDictionary.TryGetValue(pagesCorrectOrdered[i], out Rule? rule) && rule != null)
                    {
                        if (rule.PagesBefore.Contains(page))
                        {
                            pageNewIndex = i;
                            break;
                        }
                    }
                }
                if (pageNewIndex == -1)
                    pagesCorrectOrdered.Add(page);
                else
                    pagesCorrectOrdered.Insert(pageNewIndex, page);
            }

            return pagesCorrectOrdered.ToArray();
        }

        private int GetMiddleNumber(string[] update)
        {
            var middleIndex = (update.Length - 1) / 2;
            var middleNumberString = update[middleIndex];
            return int.Parse(middleNumberString);
        }
    }
}
