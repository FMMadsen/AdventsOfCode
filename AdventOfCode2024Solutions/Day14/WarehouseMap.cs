using AdventOfCode2024Solutions.Day04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day14
{
    public class WarehouseMap
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public Robot[] Robots { get; set; } = Array.Empty<Robot>();

        public void RunFor(int duraction)
        {
            foreach (Robot aRobot in Robots)
            {
                aRobot.Location = CalcEndPos(aRobot, duraction);
                if (Width <= aRobot.Location.X)
                {
                    Console.WriteLine("Location " + aRobot.Location.ToString() + " out of X bounds");
                }

                if (Height <= aRobot.Location.Y)
                {
                    Console.WriteLine("Location " + aRobot.Location.ToString() + " out of Y bounds");
                }
                
            }
            
            
        }

        protected Vector2I CalcEndPos(Robot robot, int duraction)
        {
            return new Vector2I(
                ContainOverflow(robot.Location.X + (robot.Velocity.X * duraction), Width), 
                ContainOverflow(robot.Location.Y + (robot.Velocity.Y * duraction), Height)
                );
        }

        protected int ContainOverflow(int endPos, int maxLimit)
        {
            return endPos - (maxLimit * ( (int)Math.Floor( (double)endPos / maxLimit) ));
        }
    }
}
