using AdventOfCode2024Solutions.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day12
{
    public class Fence
    {
        public Vector2I[] BelongsToIndex { get; set; }
        public Vector2I Direction;

        public Fence()
        {
            BelongsToIndex = Array.Empty<Vector2I>();
        }
    }
}
