using System.Collections.Generic;

namespace AdventOfCode2023Solutions.Day10
{
    public class Pipe
    {
        public PipeVector2 Location { get; set; }
        public PipeDirection Direction { get; set; }
        public Dictionary<PipeVector2, Pipe?> Connections { get; set; } = new Dictionary<PipeVector2, Pipe?>();
    }


}
