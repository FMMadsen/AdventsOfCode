using AdventOfCode2024Solutions.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day06
{
    public class Transform
    {
        public Vector2I Location { get; set; }
        public Vector2I Direction { get; set; }

        public Transform() 
        {
            Location = new Vector2I();
            Direction = new Vector2I();
        }

        public Transform(Vector2I location, Vector2I direction)
        {
            Location = location;
            Direction = direction;
        }
    }
}
