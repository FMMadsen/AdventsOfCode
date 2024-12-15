using AdventOfCode2024Solutions.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day15
{
    public class WarehouseObject
    {
        public Vector2I Location { get; set; } = Vector2I.Zero;
        public Vector2I ExtraSize { get; set; } = Vector2I.Zero;
        public bool Pushable { get; set; } = true;
    }
}
