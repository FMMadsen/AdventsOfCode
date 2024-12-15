namespace AdventOfCode2024Solutions.Day05
{
    public class Rule
    {
        public string CurrentPage { get; set; }
        public List<string> PagesAfter { get; set; } = [];
        public List<string> PagesBefore { get; set; } = [];

        public Rule(string currentPage)
        {
            CurrentPage = currentPage;
        }
    }
}
