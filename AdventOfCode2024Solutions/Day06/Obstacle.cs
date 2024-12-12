using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day06
{
    public class Obstacle
    {
        public Transform TheTransform { get; set; }

        public Obstacle() 
        {
            TheTransform = new Transform();
        }
    }
}
