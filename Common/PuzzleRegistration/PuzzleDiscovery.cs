using System.Reflection;

namespace Common.PuzzleRegistration
{
    public static class PuzzleDiscovery
    {
        public static IEnumerable<PuzzleDescriptor> DiscoverAll()
        {
            LoadSolutionAssembliesFromBaseDirectory();

            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name?.StartsWith("AdventOfCode") == true
                            && a.GetName().Name?.EndsWith("Solutions") == true)
                .SelectMany(asm => GetSolutionTypes(asm), (asm, type) => new SolutionCandidate(asm, type))
                .Where(c => c.IsRegistrablePuzzle)
                .Select(c => c.ToDescriptor())
                .OrderBy(p => p.Year).ThenBy(p => p.Day)
                .ToList();
        }

        private sealed class SolutionCandidate
        {
            public Assembly Assembly { get; }
            public Type SolutionType { get; }
            public PuzzleAttribute? Puzzle { get; }

            public SolutionCandidate(Assembly assembly, Type solutionType)
            {
                Assembly = assembly;
                SolutionType = solutionType;
                Puzzle = solutionType.GetCustomAttribute<PuzzleAttribute>();
            }

            public bool IsRegistrablePuzzle =>
                Puzzle != null
                && SolutionType is { IsClass: true, IsAbstract: false, IsPublic: true }
                && SolutionType.GetConstructor(Type.EmptyTypes) != null;

            public PuzzleDescriptor ToDescriptor() =>
                new(
                    Year: Puzzle!.Year,
                    Day: Puzzle.Day,
                    ProjectFolderName: Assembly.GetName().Name!,
                    Solution: (IAOCSolution)Activator.CreateInstance(SolutionType)!);
        }

        private static IEnumerable<Type> GetSolutionTypes(Assembly assembly)
        {
            IEnumerable<Type> types;
            try { types = assembly.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { types = ex.Types.OfType<Type>(); }

            return types.Where(t => typeof(IAOCSolution).IsAssignableFrom(t));
        }

        /// <summary>
        /// In case not all solution dll's has been loaded yet - then load them now
        /// </summary>
        private static void LoadSolutionAssembliesFromBaseDirectory()
        {
            var baseDir = AppContext.BaseDirectory;
            if (!Directory.Exists(baseDir)) return;

            // Keep track of what has been loaded already, so that we don't attempt to load twice
            var alreadyLoaded = new HashSet<string>(
                AppDomain.CurrentDomain.GetAssemblies()
                    .Select(a => a.GetName().Name)
                    .Where(n => n != null)!,
                StringComparer.OrdinalIgnoreCase);

            // Load those that was not yet loaded
            foreach (var dll in Directory.EnumerateFiles(baseDir, "AdventOfCode*Solutions.dll"))
            {
                var name = Path.GetFileNameWithoutExtension(dll);
                if (alreadyLoaded.Contains(name)) continue;
                try { Assembly.LoadFrom(dll); }
                catch { /* ignore load failures; discovery will simply miss that assembly */ }
            }
        }
    }
}
