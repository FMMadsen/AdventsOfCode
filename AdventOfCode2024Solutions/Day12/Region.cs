using AdventOfCode2024Solutions.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day12
{
    public class Region
    {
        public char Id;
        public Vector2I[] Area;
        public int FenceDistance 
        { 
            get
            {
                int count = 0;
                foreach (var item in Fences)
                {
                    count += item.BelongsToIndex.Length;
                }
                return count;
            } 
        }
        public Fence[] Fences;

        public Region(char id) 
        {
            Id = id;
            Area = Array.Empty<Vector2I>();
            Fences = Array.Empty<Fence>();
        }
    }
}
