using AdventOfCode2024Solutions.Day16.SolutionB.Tiles;
using ToolsFramework.Map;

namespace AdventOfCode2024Solutions.Day16.SolutionB
{
    public class Raindeer(RaindeerMaze maze)
    {
        private long lowestScore = long.MaxValue;
        public long LowestScore => lowestScore;

        private readonly long scoreForMove = 1;
        private readonly long scoreForTurn = 1000;

        private long raindeerTotalStepsTaken = 0;
        public long totalSolutionsFound = 0;

        private readonly Dictionary<string, long> cachedJunctionExitScores = [];

        public long NumberOfBestSeats => bestSeats.Count;
        private readonly HashSet<PathTile> bestSeats = [];
        private readonly Stack<PathTile> pathStack = new();

        public long CreateRoute(GenericDirection startDirection)
        {
            ArrivedAtPosition(maze.StartLocation, startDirection);
            return LowestScore;
        }

        private void ArrivedAtPosition(
            PathTile pathTile,
            GenericDirection facingDirection,
            long currentMoveScore = 0,
            string currentMovePath = "",
            string visitedJunctionsInCurrentPath = "")
        {
            pathStack.Push(pathTile);

            if (Solution.WriteDebugInfoToConsole_PrintMapEveryStep)
                currentMovePath += pathTile.Id;

            if (pathTile.IsJunction)
                visitedJunctionsInCurrentPath += pathTile.Id;

            if (Solution.WriteDebugInfoToConsole_PrintMapEveryStep)
                Solution.PrintMapToConsole(maze.MapTiles, currentMoveScore, pathTile, currentMovePath);

            if (pathTile.IsEndLocation)
                EndLocationReached(pathTile, currentMoveScore, currentMovePath);
            else
                ScoutDirectionsAndMoveFurther(pathTile, facingDirection, currentMoveScore, currentMovePath, visitedJunctionsInCurrentPath);

            pathStack.Pop();
        }

        private void EndLocationReached(
            PathTile mapTile,
            long currentMoveScore,
            string currentMovePath)
        {
            totalSolutionsFound++;
            var printMap = false;

            if (currentMoveScore == lowestScore)
            {
                foreach (var tile in pathStack)
                {
                    bestSeats.Add(tile);
                }
            }
            else if (currentMoveScore < lowestScore)
            {
                lowestScore = currentMoveScore;
                bestSeats.Clear();
                foreach (var tile in pathStack)
                {
                    bestSeats.Add(tile);
                }

                if (Solution.WriteDebugInfoToConsole_SolutionsScoreWhenSmaller)
                {
                    Solution.PrintSolutionsCount(currentMoveScore, totalSolutionsFound, lowestScore, raindeerTotalStepsTaken);
                }

                if (Solution.WriteDebugInfoToConsole_PrintMapEverySolutionWhenSmaller)
                    printMap = true;
            }

            if (printMap || Solution.WriteDebugInfoToConsole_PrintMapEverySolution)
                Solution.PrintMapToConsole(maze.MapTiles, currentMoveScore, mapTile, currentMovePath);

            if (Solution.WriteDebugInfoToConsole_SolutionsCount)
                Solution.PrintSolutionsCount(currentMoveScore, totalSolutionsFound, lowestScore, raindeerTotalStepsTaken);

            if (Solution.WriteDebugInfoToConsole_SolutionsScore)
                Solution.PrintNewSolutionScore(currentMoveScore);
        }

        private void ScoutDirectionsAndMoveFurther(PathTile currentPathTile, GenericDirection currentDirection, long currentMoveScore, string currentMovePath, string visitedJunctionsInCurrentPath)
        {
            var newTile = currentPathTile.GetTile(currentDirection);
            var newDirection = currentDirection;
            var newScore = currentMoveScore;
            AttemptMoveTo(currentPathTile, newTile, newDirection, newScore, currentMovePath, visitedJunctionsInCurrentPath);

            newTile = currentPathTile.GetTileLeftOf(currentDirection);
            newDirection = GenericDirectionMapper.GetLeftDirection(currentDirection);
            newScore = currentMoveScore + scoreForTurn;
            AttemptMoveTo(currentPathTile, newTile, newDirection, newScore, currentMovePath, visitedJunctionsInCurrentPath);

            newTile = currentPathTile.GetTileRightOf(currentDirection);
            newDirection = GenericDirectionMapper.GetRightDirection(currentDirection);
            newScore = currentMoveScore + scoreForTurn;
            AttemptMoveTo(currentPathTile, newTile, newDirection, newScore, currentMovePath, visitedJunctionsInCurrentPath);
        }



        private void AttemptMoveTo(
            PathTile currentPathTile,
            GenericMapTile? newMapTile,
            GenericDirection newDirection,
            long currentMoveScore,
            string currentMovePath,
            string visitedJunctionsInCurrentPath)
        {
            if (newMapTile is PathTile newPathTile && !newPathTile.IsBlocked)
            {
                if (currentPathTile.IsJunction)
                {
                    if (HasJunctionAndDirectionBeenVisitedBeforeWithLowerOrSameScore(currentPathTile, newDirection, currentMoveScore))
                        return;
                }

                if (newPathTile.IsJunction)
                {
                    if (HasVisitedJunctionBefore(newPathTile, visitedJunctionsInCurrentPath))
                        return;
                }

                raindeerTotalStepsTaken++;
                var newScore = currentMoveScore + scoreForMove;
                ArrivedAtPosition(newPathTile, newDirection, newScore, currentMovePath, visitedJunctionsInCurrentPath);
            }
        }

        private static bool HasVisitedJunctionBefore(
            PathTile mapTile,
            string visitedJunctionsInCurrentPath)
        {
            return visitedJunctionsInCurrentPath.Contains(mapTile.Id);
        }

        private bool HasJunctionAndDirectionBeenVisitedBeforeWithLowerOrSameScore(
            PathTile currentPathTile,
            GenericDirection newDirection,
            long currentMoveScore)
        {
            var cacheKey = currentPathTile.GetTileAndDirectionCacheKey(newDirection);

            if (cachedJunctionExitScores.TryGetValue(cacheKey, out long existingScore))
            {
                if (currentMoveScore > existingScore)
                    return true;
                else
                    cachedJunctionExitScores[cacheKey] = currentMoveScore;
            }
            else
                cachedJunctionExitScores.Add(cacheKey, currentMoveScore);

            return false;
        }
    }
}