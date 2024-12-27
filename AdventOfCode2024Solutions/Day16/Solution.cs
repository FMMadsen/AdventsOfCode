using AdventOfCode2024Solutions.Day04;
using AdventOfCode2024Solutions.Day06;
using Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;

namespace AdventOfCode2024Solutions.Day16
{
    public enum MapCharD16
    {
        Empty = '.',
        Spawn = 'S',
        Exit = 'E',
        Wall = '#',
        NN = '^',
        EE = '>',
        SS = 'v',
        WW = '<',
        Path = 'O',
        Walkable = Empty|NN|EE|SS|WW|Path|Spawn|Exit
    }

    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 16: Labyrinth Pathing";

        private static int TheadsAmountValue = 1;
        public static int TheadsAmount { 
            get 
            { 
                return TheadsAmountValue != 1 ? 1 : TheadsAmountValue;
            } 
        }

        public static int FrameTime = 30;
        public static int ComputeDelay = 0;
        protected static bool Printing = false;

        private static string[] Canvas = Array.Empty<string>();

        private static Spawn Start = new Spawn();
        private static Mapexit End = new Mapexit();
        private StringMap Map = StringMap.Empty;
        private int PrintMapIndex = -1;
        private bool Running = false;
        private readonly Task[] RunningTasks = new Task[TheadsAmount];
        private readonly bool[] IdleTasks = new bool[TheadsAmount];
        private Task RenderTask = Task.CompletedTask;


        public Dictionary<MapCharD16, Func<GameObject>> CharToTypeList = new Dictionary<MapCharD16, Func<GameObject>>() {
            { MapCharD16.Empty, ()=>{return new Walkable(); } },
            { MapCharD16.Wall, ()=>{return new Wall(); } },
            { MapCharD16.Spawn, ()=>{return new Spawn(); } },
            { MapCharD16.Exit, ()=>{return new Mapexit(); } }
        };
        public Dictionary<char, MapCharD16> CharToEnumList = new Dictionary<char, MapCharD16>(){
            {'.', MapCharD16.Empty },
            {'#', MapCharD16.Wall},
            {'S', MapCharD16.Spawn},
            {'E', MapCharD16.Exit}
        };

        public string SolvePart1(string[] datasetLines)
        {
            Map = new StringMap(datasetLines.Select(a=> a.TrimEnd()).ToArray(), CharToTypeList, CharToEnumList);
            Canvas = Map.Map.ToArray();

            FindStartAndEnd();

            Map.Spawn(Start);
            Map.Spawn(End);

            RunPathFinding();

            Path[] possibleRoutes = Map.GetAllAt(Start.Transform.Location).Where(a => a is Path).Select(a => (Path)a).ToArray();
            long count = -1;


            if (0 < possibleRoutes.Length)
            {
                long[] possibleRoutesScores = new long[possibleRoutes.Length];

                count = possibleRoutes.First().ScoreToMapexit();

                Path bestRoute = possibleRoutes.First();

                for (int i = 1; i < possibleRoutes.Length; i++)
                {
                    var tempRouteScore = possibleRoutes[i].ScoreToMapexit();

                    if (tempRouteScore < count)
                    {
                        count = tempRouteScore;
                        bestRoute = possibleRoutes[i];
                    }

                }

                if (!Console.IsOutputRedirected)
                {
                    ClearCanvasAndPrintBestRoute(bestRoute);
                }
                
            }



            Map.Destroy();
            return count.ToString();
        }

        private void RunPathFinding()
        {
            // start at exit
            Walkable[] firstSteps = Map.FindNeighborsOf<Walkable>(End.Transform.Location);

            foreach (Walkable step in firstSteps)
            {
                Map.Spawn(new Path(step.Transform, Map));
            }

            Running = true;

            for (int i = 0; i < RunningTasks.Length; i++)
            {
                RunningTasks[i] = ExpandPathTask(i);
                RunningTasks[i].Start();
            }

            if (!Console.IsOutputRedirected)
            {
                Console.WriteLine("");
                PrintMapIndex = Console.CursorTop;
                try
                {
                    Console.SetWindowPosition(0, PrintMapIndex);
                }
                catch (IOException) { }

                for (int i = 0; i < Canvas.Length; i++) 
                { 
                    Console.WriteLine(String.Concat(IntToLeast3DigitString(i), " ", Canvas[i]));
                }

                RenderTask = NewRenderTask();

                Console.CursorVisible = false;
                RenderTask.Start();
            }

            Task.WaitAll(RunningTasks);
            RenderTask.Wait();

            if (!Console.IsOutputRedirected)
            {
                PrintCanvas();
                Console.CursorVisible = true;
            }
        }

        private Task ExpandPathTask(int taskIndex)
        {
            return new Task(() =>
            {
                //IEnumerable<Path> paths = Map.World.GetChildren<Path>().Where(a=> PathStatus.Idle == a.Status);
                Path[] possiblePaths;
                Path? firstIdle;

                while (Running)
                {
                    lock (Map.World.Children) 
                    { 
                        firstIdle = Map.World.GetChildren<Path>().Where(a => PathStatus.Idle == a.Status).OrderBy(a=> a.ScoreToMapexit()).FirstOrDefault()?.Grab(); 
                    }

                    if (null == firstIdle)
                    {
                        IdleTasks[taskIndex] = true;

                        if (!IdleTasks.Any(a=> false == a)) { Running = false; }

                        Task.Delay(1).Wait(); 
                        continue;
                    }
                    else
                    {
                        IdleTasks[taskIndex] = false;
                    }

                    /*(
                        a.Id != firstIdle.Id
                        && PathStatus.Idle != a.Status
                        && PathStatus.Destroying != a.Status
                        && a.Exit == firstIdle.Exit
                        )*/

                    /*if (Map.World.GetChildren<Path>().Any(a =>
                        a.Id != firstIdle.Id
                        && (PathStatus.Ready == a.Status
                        || PathStatus.Finalizing == a.Status)
                        && a.Transform.Location == firstIdle.Exit.Location
                        && a.Transform.Direction + firstIdle.Exit.Direction == Vector2I.Zero

                    ))
                    {
                        firstIdle.Parent?.DestroyChild(firstIdle);
                        continue;
                    }*/

                    /*if (firstIdle.Exit.Location == firstIdle.Transform.Location + firstIdle.Transform.Direction
                    && firstIdle.Exit.Direction == firstIdle.Transform.Direction)
                    {
                        Console.WriteLine("firstIdle exit is correct");
                    }*/

                    possiblePaths = firstIdle.Expand();



                    if (PathStatus.Ready != firstIdle.Status)
                    { continue; }

                    PostExpandPath(firstIdle, possiblePaths);

                }
            });
        }

        private void PostExpandPath(Path aPath, Path[] possiblePaths)
        {
            
            
            foreach (Path pPath in possiblePaths)
            {

                Path[] pathsOnLocation = Map.GetAllAt(aPath.Transform.Location).Where(a => a is Path).Select(a => (Path)a).ToArray();
                Path? OppositePath = pathsOnLocation.Where(a => a.Transform.Direction == pPath.Exit.Direction.TurnRight().TurnRight()).FirstOrDefault();

                SetFavoredPath(pPath, 0 < pathsOnLocation.Length ? pathsOnLocation : [aPath]);

                if (null != OppositePath
                    && OppositePath.ScoreToMapexit() < aPath.ScoreToMapexit())
                {
                    continue;
                }

                if (pPath.Id == Guid.Empty)
                {
                    Map.Spawn(pPath);
                }
            }
        }

        public void SetFavoredPath(Path pathToSet, Path[] possibleBest)
        {
            if (0 == possibleBest.Length) { throw new ArgumentException("Path[] possibleBest can not be empty"); }

            IEnumerable<Path> bestPaths = Enumerable.Empty<Path>();

            bestPaths = bestPaths.Append(possibleBest.First());

            for (int i=1; i < possibleBest.Length; i++)
            {
                Path[] shortest = ShortestOrEqualPath(possibleBest[i], bestPaths.First(), pathToSet.Exit);

                if (2 == shortest.Length)
                {
                    bestPaths = bestPaths.Append(possibleBest[i]);
                }
                else
                {
                    if (shortest.First().Id != bestPaths.First().Id)
                    {
                        bestPaths = Enumerable.Empty<Path>().Append(possibleBest[i]);
                    }
                }
            }

            pathToSet.NextFavored = bestPaths.ToArray();
        }

        public static Path GetFavoredPath(Path[] pathsStartingSame, Transform fromPathExit)
        {
            if (pathsStartingSame.Length < 1) { throw new ArgumentNullException("GetFavoredPath require at least 1 entry in argument Path[] pathsStartingSame."); }

            Path favoredPath = pathsStartingSame.First();

            for(int i = 1; i < pathsStartingSame.Length; i++)
            {
                Path onSpotPath = pathsStartingSame[i];

                if (PathStatus.Ready == onSpotPath.Status || PathStatus.Finalizing == onSpotPath.Status)
                {
                    favoredPath = ShortestPath(onSpotPath, favoredPath, fromPathExit);
                }
            }

            return favoredPath;
        }

        public static Path ShortestPath(Path a, Path b, Transform fromPathExit)
        {
            return a.ScoreToMapexit() + Path.GetTurnScore(fromPathExit.Direction, a.Transform.Direction) < b.ScoreToMapexit() + Path.GetTurnScore(fromPathExit.Direction, b.Transform.Direction) ? a : b;
        }

        public static Path[] ShortestOrEqualPath(Path a, Path b, Transform fromPathExit)
        {
            long aScore = a.ScoreToMapexit() + Path.GetTurnScore(fromPathExit.Direction, a.Transform.Direction);
            long bScore = b.ScoreToMapexit() + Path.GetTurnScore(fromPathExit.Direction, b.Transform.Direction);

            if (aScore < bScore)
            {
                return [a];
            }
            else if (bScore < aScore)
            {
                return [b];
            }

            return [ a, b ];
        }

        private void FindStartAndEnd()
        {
            bool startFound = false;
            bool endFound = false;

            for (int y = 0; y < Map.Map.Length; y++)
            {
                for (int x = 0; x < Map.Map.First().Length; x++)
                {
                    char symbol = Map.CharOf(new Vector2I(x, y));

                    if ((char)MapCharD16.Spawn == symbol) 
                    { 
                        Start.Transform = new Transform(new Vector2I(x, y), Vector2I.EE);
                        startFound = true;
                    }
                    else if ((char)MapCharD16.Exit == symbol)
                    {
                        End.Transform = new Transform( new Vector2I(x, y), Vector2I.EE);
                        endFound = true;
                    }
                }

                if (startFound && endFound)
                {
                    return;
                }
            }
        }

        private Task NewRenderTask() 
        {
            return new Task(() =>
            {
                while (Running)
                {
                    try
                    {
                        PrintCanvas();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + "\n" + e.StackTrace + "\n" + e.InnerException?.Message);
                    }

                    
                    Task.Delay(FrameTime).Wait();
                }
            });
        }

        public static string IntToLeast3DigitString(int value)
        {
            return value < 10 ? "00" + value.ToString() : (value < 100 ? "0" + value.ToString() : value.ToString());
        }

        public static char? DirectionToChar(Vector2I direction)
        {
            char? symbol = null;

            if (Vector2I.NN == direction)
            {
                symbol = (char)MapCharD16.NN;
            }
            else if (Vector2I.EE == direction)
            {
                symbol = (char)MapCharD16.EE;
            }
            else if (Vector2I.SS == direction)
            {
                symbol = (char)MapCharD16.SS;
            }
            else if (Vector2I.WW == direction)
            {
                symbol = (char)MapCharD16.WW;
            }

            return symbol;
        }

        public static void UpdateCanvas<T>(T gameObject) where T : GameObject
        {
            char? symbol = null;

            if (gameObject is Walkable)
            {
                symbol = DirectionToChar(gameObject.Transform.Direction);
            }
            else if (gameObject is Path && gameObject.Transform.Location != Start.Transform.Location)
            {
                symbol = (char)MapCharD16.Path;
            }

            if (null != symbol)
            {
                lock (Canvas)
                {
                    Canvas[gameObject.Transform.Location.Y] = String.Concat(Canvas[gameObject.Transform.Location.Y].Substring(0, gameObject.Transform.Location.X),
                                                                    symbol,
                                                                    Canvas[gameObject.Transform.Location.Y].Substring(gameObject.Transform.Location.X + 1));

                }
            }

        }

        public void PrintCanvas()
        {
            if (Printing) { return; }

            var cursorPos = Console.GetCursorPosition();
            Printing = true;

            for (int y = 0; y < Canvas.Length; y++)
            {
                Console.SetCursorPosition(0, y + PrintMapIndex);
                try
                {
                    Console.SetWindowPosition(0, PrintMapIndex);
                }
                catch (IOException) { }
                Console.Write(String.Concat(IntToLeast3DigitString(y), " ", Canvas[y]));
            }

            Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
            try
            {
                Console.SetWindowPosition(0, PrintMapIndex);
            }
            catch (IOException) { }
            
            Printing = false;
        }

        public void PrintListAllPaths()
        {
            if (Console.IsOutputRedirected)
            { return; }

            Path[] paths = Map.GetAll<Path>();

            for (int i = 0; i < paths.Length; i++)
            {
                var path = paths[i];
                var sb = new StringBuilder();

                sb.Append(IntToLeast3DigitString(i));

                sb.Append(" O");

                Walkable[] children = path.GetChildren<Walkable>();

                foreach (Walkable step in children)
                {
                    sb.Append(DirectionToChar(step.Transform.Direction));
                }

                sb.Append("\nLength: ");
                sb.Append(children.Length+1);

                sb.Append(" Pos: ");
                sb.Append(path.Transform.Location.ToString());
                sb.Append(">>");
                sb.Append(path.Exit.Location.ToString());

                sb.Append("\nScore: ");
                sb.Append(path.Score);
                sb.Append(" To exit: ");
                sb.Append(path.ScoreToMapexit());
                sb.Append('\n');

                Console.WriteLine(sb.ToString());
            }
        }

        public void ClearCanvasAndPrintBestRoute(Path routeBegin)
        {
            if (!Console.IsOutputRedirected)
            { return; }

            lock (Canvas)
            {
                Canvas = Map.Map.ToArray();
            }

            Path? nextPath = routeBegin;

            while (nextPath is Path currentPath)
            {
                UpdateCanvas(currentPath);

                foreach (Walkable step in currentPath.GetChildren<Walkable>()) 
                {
                    UpdateCanvas(step);
                }
                
                nextPath = currentPath.NextFavored.FirstOrDefault();
            }

            
            PrintCanvas();

        }

        public string SolvePart2(string[] datasetLines)
        {
            
            Canvas = Array.Empty<string>();
            Start = new Spawn();
            End = new Mapexit();
            Map = StringMap.Empty;
            PrintMapIndex = -1;
            Running = false;
            for(int i = 0; i < IdleTasks.Length; i++)
            {
                    RunningTasks[i] = Task.CompletedTask;
                    IdleTasks[i] = false;
            }
            RenderTask = Task.CompletedTask;

            SolvePart1(datasetLines);

            return CalcAllBestRoutes().ToString();
        }

        public long CalcAllBestRoutes() 
        {
            var startPaths = Map.GetAllAt(Start.Transform.Location).Where(a=> a is Path).Select(a=> (Path)a).OrderBy(a=> a.ScoreToMapexit()).ToArray();

            IEnumerable<Guid> used = Enumerable.Empty<Guid>();
            IEnumerable<Vector2I> locationUsed = Enumerable.Empty<Vector2I>();

            Path[] nextPaths;
            {
                long score = startPaths.First().ScoreToMapexit();
                IEnumerable<Path> tempPaths = Enumerable.Empty<Path>().Append(startPaths.First());

                for (int i = 1; i < startPaths.Length; i++)
                {
                    if (score < startPaths[i].ScoreToMapexit())
                    {
                        break;
                    }

                    tempPaths = tempPaths.Append(startPaths[i]);
                }

                nextPaths = tempPaths.ToArray();
            }

            while (0 < nextPaths.Length)
            {
                IEnumerable<Path> tempPaths = Enumerable.Empty<Path>();

                for (int i = 0; i < nextPaths.Length; i++)
                {
                    Path currentPath = nextPaths[i];

                    if (!locationUsed.Any(a => a == currentPath.Transform.Location) 
                        && !used.Any(a => a == currentPath.Id))
                    {
                        locationUsed = locationUsed.Append(currentPath.Transform.Location);
                        used = used.Append(currentPath.Id);
                    }
                    

                    foreach (Walkable step in currentPath.GetChildren<Walkable>())
                    {
                        if (!used.Any(a => a == step.Id))
                        {
                            used = used.Append(step.Id);
                        }
                    }

                    foreach (Path n in currentPath.NextFavored)
                    {
                        tempPaths = tempPaths.Append(n);
                    }
                }


                nextPaths = tempPaths.ToArray();

            }

            if (!used.Any(a => a == End.Id))
            {
                used = used.Append(End.Id);
            }

            return used.Count();
        }
    }
}
