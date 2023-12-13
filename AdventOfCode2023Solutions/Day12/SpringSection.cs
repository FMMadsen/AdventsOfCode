namespace AdventOfCode2023Solutions.Day12
{
    public class SpringSection
    {
        public string SpringsString { get; set; } = string.Empty;
        public char[] Springs { get; set; } = [];

        public string FollowingSpringsString { get; set; } = string.Empty;
        public char[] FollowingSprings { get; set; } = [];

        public SpringSection? SectionBefore { get; set; } = null;
        public SpringSection? SectionAfter { get; set; } = null;

    }
}
