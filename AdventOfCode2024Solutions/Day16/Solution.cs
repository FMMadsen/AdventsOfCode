using AdventOfCode2024Solutions.Day04;
using Common;
using System.Diagnostics.Metrics;

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

        private static int TheadsAmountValue = 4;
        public static int TheadsAmount { 
            get 
            { 
                return TheadsAmountValue < 2 ? 2 : TheadsAmountValue;
            } 
        }

        public int FrameTime = 100;
        public static int ComputeDelay = 0;

        private static string[] Canvas = Array.Empty<string>();

        //private Path[] Paths = Array.Empty<Path>();
        private static Spawn Start = new Spawn();
        private static Mapexit End = new Mapexit();
        private StringMap Map = StringMap.Empty;
        private int PrintMapIndex = 0;
        private bool Running = false;
        private Task[] RunningTasks = new Task[TheadsAmount-1];
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
            Map = new StringMap(datasetLines, CharToTypeList, CharToEnumList);
            Canvas = Map.Map.ToArray();

            FindStartAndEnd();

            Map.Spawn(Start);
            Map.Spawn(End);

            RunPathFinding();

            Path[] possibleRoutes = Map.GetAllAt(Start.Transform.Location).Where(a=> a is Path).Select(a=> (Path)a).ToArray();

            long[] possibleRoutesScores = new long[possibleRoutes.Length];

            long count = -1;

            for (int i = 0; i < possibleRoutes.Length; i++)
            {
                possibleRoutesScores[i] = possibleRoutes[i].ScoreToMapexit() + Path.GetTurnScore(Start.Transform.Direction, possibleRoutes[i].Transform.Direction);

                count = 0 == i ? count = possibleRoutesScores[i] : (possibleRoutesScores[i] < count ? possibleRoutesScores[i] : count);
                
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

            if (!Console.IsOutputRedirected)
            {
                
                Running = true;
                RenderTask = NewRenderTask();

                foreach (string line in Map.Map) 
                { 
                    Console.WriteLine(line);
                }
            }

            for (int i = 0; i < RunningTasks.Length; i++)
            {
                RunningTasks[i] = ExpandPathTask();
                RunningTasks[i].Start();
            }

            if (!Console.IsOutputRedirected)
            {
                PrintMapIndex = Console.CursorTop;
                Console.CursorVisible = false;
                RenderTask.Start();
            }

            Task.WaitAll(RunningTasks);

            Running = false;

            RenderTask.Wait();

            if (!Console.IsOutputRedirected)
            {
                PrintMap();
                Console.CursorVisible = true;
            }
        }

        private Task ExpandPathTask()
        {
            return new Task(() =>
            {
                IEnumerable<Path> paths = Map.World.GetChildren<Path>().Where(a=> PathStatus.Idle == a.Status);
                Path[] possiblePaths;
                Path? firstIdle;
                bool anyIdle = true;

                while (anyIdle)
                {
                    lock (Map.World.Children) 
                    { 
                        firstIdle = Map.World.GetChildren<Path>().Where(a => PathStatus.Idle == a.Status).FirstOrDefault()?.Grab(); 
                    }

                    if (null == firstIdle)
                    { anyIdle = false; break; }

                    if (Map.World.GetChildren<Path>().Any(a =>
                        a.Id != firstIdle.Id
                        && PathStatus.Idle != a.Status
                        && PathStatus.Destroying != a.Status
                        && a.Exit.Location == firstIdle.Exit.Location
                        && a.Exit.Direction == firstIdle.Exit.Direction
                    ))
                    {
                        firstIdle.Parent?.DestroyChild(firstIdle);
                        continue;
                    }

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
            GameObject[] inhabitantsOnSpot = Map.GetAllAt(aPath.Transform.Location).
                Where(a=> a.Id != aPath.Id).ToArray();
            Path favoredPath = aPath;

            foreach (GameObject onSpot in inhabitantsOnSpot) 
            {// every Path that begin at same location, check favorable,
             // if is shorter then remove possiblePath in this direction
                if (onSpot is Path onSpotPath && PathStatus.Ready == onSpotPath.Status)
                {
                    long onSpotScore = onSpotPath.ScoreToMapexit();
                    long aPathScore = aPath.ScoreToMapexit();

                    favoredPath =  ShortestPath(onSpotPath, favoredPath);

                    if (favoredPath.Id != aPath.Id )
                    {
                        possiblePaths = possiblePaths.Where(a=> favoredPath.Transform.Direction + a.Transform.Direction != Vector2I.Zero).ToArray();
                    }
                }
            }

            foreach (Path pPath in possiblePaths)
            {
                bool skip = false;


                GameObject[] inhabitants = Map.GetAllAt(pPath.Transform.Location);

                foreach (GameObject inhabitant in inhabitants)
                {// set favored for those who exit on aPath
                    Path? inhabitedPath = null;

                    if (inhabitant is Walkable step && step.Parent is Path parent)
                    { inhabitedPath = parent; }
                    
                    else if (inhabitant is Path inhab)
                    { inhabitedPath = inhab; }

                    if (null != inhabitedPath)
                    {
                        if (inhabitedPath.Exit.Location == aPath.Transform.Location)
                        { inhabitedPath.NextFavored = favoredPath; }

                        if (inhabitedPath.Exit == pPath.Exit)
                        { skip = true; }
                    }
                    
                }

                if (!skip)
                {
                    pPath.NextFavored = favoredPath ?? aPath;
                    Map.Spawn(pPath);
                }
                
            }
        }

        public static Path ShortestPath(Path a, Path b)
        {
            return a.ScoreToMapexit() < b.ScoreToMapexit() ? a : b;
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
                        Start.Transform.Location = new Vector2I(x, y);
                        Start.Transform.Direction = Vector2I.EE;
                        startFound = true;
                    }
                    else if ((char)MapCharD16.Exit == symbol)
                    {
                        End.Transform.Location = new Vector2I(x, y);
                        End.Transform.Direction = Vector2I.EE;
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
                        PrintMap();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + "\n" + e.StackTrace + "\n" + e.InnerException?.Message);
                    }

                    
                    Task.Delay(FrameTime).Wait();
                }
            });
        }

        public static void UpdateCanvas<T>(T gameObject) where T : GameObject
        {
            char? symbol = null;

            if (gameObject is Walkable)
            {
                if (Vector2I.NN == gameObject.Transform.Direction)
                {
                    symbol = (char)MapCharD16.NN;
                }
                else if (Vector2I.EE == gameObject.Transform.Direction)
                {
                    symbol = (char)MapCharD16.EE;
                }
                else if (Vector2I.SS == gameObject.Transform.Direction)
                {
                    symbol = (char)MapCharD16.SS;
                }
                else if (Vector2I.WW == gameObject.Transform.Direction)
                {
                    symbol = (char)MapCharD16.WW;
                }

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

        public void PrintMap()
        {
            for (int y = 0; y < Canvas.Length; y++)
            {
                Console.SetCursorPosition(0, y + PrintMapIndex);
                Console.Write(Canvas[y]);
            }

            Console.SetCursorPosition(0, Console.CursorTop);
        }

        public string SolvePart2(string[] datasetLines)
        {
            return "To be implemented";
        }
    }
}
