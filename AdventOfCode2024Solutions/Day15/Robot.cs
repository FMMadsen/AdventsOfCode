using AdventOfCode2024Solutions.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day15
{
    public class Robot : WarehouseObject
    {
        public Vector2I Direction { get; set; } = Vector2I.NN;

        public Vector2I[] StepList { get; set; } = Array.Empty<Vector2I>();
    }
}
