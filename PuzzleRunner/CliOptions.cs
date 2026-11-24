using Common.PuzzleRegistration;

namespace PuzzleRunner
{
    internal sealed class CliOptions
    {
        public int? Year { get; init; }
        public int? Day { get; init; }
        public bool All { get; init; }

        public const string UsageText =
            "Usage: PuzzleRunner [--all] [--year YYYY] [--day DD]\n" +
            "  (no args)             run latest discovered year\n" +
            "  --all                 run every discovered puzzle\n" +
            "  --year 2024           run every puzzle in that year\n" +
            "  --year 2024 --day 5   run one puzzle";

        public IEnumerable<PuzzleDescriptor> Filter(IEnumerable<PuzzleDescriptor> all)
        {
            var list = all.ToList();
            if (All) return list;

            int year = Year ?? list.Max(p => p.Year);
            var byYear = list.Where(p => p.Year == year);
            return Day is int d ? byYear.Where(p => p.Day == d) : byYear;
        }

        public static bool TryParse(string[] args, out CliOptions options, out string error)
        {
            int? year = null, day = null;
            bool all = false;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--all":
                        all = true;
                        break;
                    case "--year" when i + 1 < args.Length && int.TryParse(args[++i], out var y):
                        year = y;
                        break;
                    case "--day" when i + 1 < args.Length && int.TryParse(args[++i], out var d):
                        day = d;
                        break;
                    case "-h" or "--help":
                        options = new CliOptions();
                        error = UsageText;
                        return false;
                    default:
                        options = new CliOptions();
                        error = $"Unrecognized argument: {args[i]}";
                        return false;
                }
            }

            options = new CliOptions { Year = year, Day = day, All = all };
            error = "";
            return true;
        }
    }
}
