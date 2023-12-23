using Common;

namespace AdventOfCode2023Solutions.Day13
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 13: Point of Incidence";

        public string SolvePart1(string[] datasetLines)
        {
            var patternNotes = SplitIntoPatterns(datasetLines);
            patternNotes.ForEach(patternNote => { patternNote.FindMirrorTotalsPart1(100, 1); });
            var sumTotals = patternNotes.Sum(p => p.Total);
            return sumTotals.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var patternNotes = SplitIntoPatterns(datasetLines);
            patternNotes.ForEach(patternNote => { patternNote.FindMirrorTotalsPart2(100, 1); });
            var sumTotals = patternNotes.Sum(p => p.Total);
            return sumTotals.ToString();
        }

        private static List<PatternNote> SplitIntoPatterns(string[] datasetLines)
        {
            List<PatternNote> notes = new List<PatternNote>();
            int patternNoteIdCounter = 0;

            List<string> parts = new List<string>();

            int linesStart = 0;
            for (int i = 0; i < datasetLines.Length; i++)
            {
                var isEmptyLine = string.IsNullOrWhiteSpace(datasetLines[i]);
                var isEndOfLines = i == datasetLines.Length - 1;

                if (isEmptyLine)
                {
                    var rows = datasetLines.Take(new Range(linesStart, i)).ToArray();
                    var pattern = new PatternNote(rows, patternNoteIdCounter++);
                    notes.Add(pattern);
                    linesStart = i + 1;
                    continue;
                }
                if (isEndOfLines)
                {
                    var rows = datasetLines.Take(new Range(linesStart, i + 1)).ToArray();
                    var pattern = new PatternNote(rows, patternNoteIdCounter++);
                    notes.Add(pattern);
                }
            }

            return notes;
        }
    }
}
