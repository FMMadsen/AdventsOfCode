using AdventOfCode2024Solutions.Day04;
using AdventOfCode2024Solutions.Day06;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day16
{
    public enum PathStatus
    {
        Idle,
        Building,
        Ready,
        Finalizing,
        Destroying
    }

    public class Path : GameObject
    {
        private long ScoreValue = 0;
        private PathStatus StatusValue = PathStatus.Idle;
        private Transform? ExitValue = null;
        private Spawn? Start = null;

        public const int StepScore = 1;
        public const int TurnScore = 1000;

        public long Score { get { return ScoreValue; } }
        
        public Path? NextFavored { get; set; }
        public PathStatus Status { get { return StatusValue; } }
        
        public Transform Exit { 
            get 
            {
                if (null == ExitValue)
                {
                    Transform stepTransform;

                    if (PathStatus.Building == Status)
                    {
                        stepTransform = Children.FirstOrDefault()?.Transform ?? Transform;
                    }
                    else
                    {
                        stepTransform = GetLastChild<Walkable>()?.Transform ?? Transform;
                    }

                    ExitValue = new Transform(stepTransform.Location + stepTransform.Direction, stepTransform.Direction);
                }
                
                return (Transform)ExitValue;
            } 
        }

        private StringMap Map { get; set; }

        public Path(Transform lastStep, StringMap map) : base()
        {
            this.Transform = lastStep;
            Map = map;
        }


        public void SetComplete()
        {
            ChildrenValue = ChildrenValue.Reverse().ToArray();

            StatusValue = PathStatus.Finalizing;

            Walkable[] childrenWalkables = GetChildren<Walkable>();

            if (0 < childrenWalkables.Length)
            {
                ScoreValue += (2 * StepScore) + GetTurnScore(Transform.Direction, ChildrenValue.First().Transform.Direction);

                for (int i = 1; i < childrenWalkables.Length; i++)
                {
                    ScoreValue += GetTurnScore(childrenWalkables[i - 1].Transform.Direction, childrenWalkables[i].Transform.Direction);

                    ScoreValue += StepScore;
                }
            }
            else
            {
                ScoreValue += StepScore;
            }



            StatusValue = PathStatus.Ready;

            Solution.UpdateCanvas(this);
        }

        public static int GetTurnScore(Vector2I from, Vector2I to)
        {
            int newScore = 0;

            if (to != from)
            {
                if (from.TurnLeft() == to
                    || from.TurnRight() == to)
                {
                    newScore = 1 * TurnScore;
                }
                else
                {
                    newScore = 2 * TurnScore;
                }
            }

            return newScore;
        }

        public long ScoreToMapexit() 
        {
            if (null == NextFavored){ return Score; }

            int connectScore = GetTurnScore(Exit.Direction, NextFavored.Transform.Direction);

            if (null != Start) { connectScore += GetTurnScore(Start.Transform.Direction, Transform.Direction); }

            return NextFavored.ScoreToMapexit() + Score + connectScore;
        }

        public void AddStepAndMove(Walkable step)
        {
            if (PathStatus.Ready == Status) { return; }

            (this.Transform, step.Transform) = (step.Transform, this.Transform);
            SpawnChild(step);

            Solution.UpdateCanvas(step);
        }

        public override void Destroy()
        {
            StatusValue = PathStatus.Destroying;
            Map = StringMap.Empty;
            base.Destroy();
        }

        protected Path[] ExpandOneStep()
        {
            if (0 < Solution.ComputeDelay) { Task.Delay(Solution.ComputeDelay).Wait(); }

            GameObject[] neighbors = Map.FindNeighborsOf<GameObject>(Transform.Location);

            Path[] previous;
            {
                IEnumerable<Path> buildPrevious= Enumerable.Empty<Path>();

                foreach (GameObject neighbor in neighbors)
                {
                    if (neighbor is Spawn)
                    {
                        AddStepAndMove(new Walkable() { Transform = neighbor.Transform });
                        Start = Map.GetAll<Spawn>().First();
                        SetComplete();
                        return Array.Empty<Path>();
                    }
                    else if (neighbor is Walkable)
                    {
                        if (neighbor.Transform.Location != Transform.Location + Transform.Direction)
                        {
                            buildPrevious = buildPrevious.Append(new Path(neighbor.Transform, Map));
                        }
                    }
                }

                previous = buildPrevious.ToArray();
            }

            if (1 < previous.Length)
            {
                SetComplete();
                return previous;
            }
            else if (1 == previous.Length)
            { 
                AddStepAndMove(new Walkable() { Transform = previous.First().Transform });
            }
            else
            {
                SetComplete();
            }

            return Array.Empty<Path>();
        }

        public Path Grab()
        {
            StatusValue = PathStatus.Building;
            return this;
        }

        public Path[] Expand()
        {
            StatusValue = PathStatus.Building;

            Path[] leftovers = Array.Empty<Path>();

            while (PathStatus.Ready != Status)
            {
                leftovers = ExpandOneStep(); 
            }

            
            for (int i = 0; i < leftovers.Length; i++)
            {// if allready in use, replace with that one

                Path inhabPath = leftovers[i];

                GameObject[] inhabitants = Map.GetAllAt(inhabPath.Transform.Location);

                foreach (GameObject inhabitant in inhabitants)
                {
                    if (inhabitant is Path mainPath && mainPath.Exit == inhabPath.Exit)
                    {
                        inhabPath.Destroy();
                        leftovers[i] = mainPath;
                    }
                    else if (inhabitant is Walkable step && step.Parent is Path secondPath && secondPath.Exit == inhabPath.Exit)
                    {
                        inhabPath.Destroy();
                        leftovers[i] = secondPath;
                    }
                }
            }

            return leftovers;
        }
    }
}
