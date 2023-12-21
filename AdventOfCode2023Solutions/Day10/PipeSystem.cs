namespace AdventOfCode2023Solutions.Day10
{
    public class PipeSystem
    {
        public Pipe StartLocation { get; private set; }
        public Pipe[,] PipeMap { get; private set; }

        public PipeSystem(string[] pipeMap)
        {
            var noOfXPipes = pipeMap.Length;
            var noOfYPipes = pipeMap[0].Length;

            PipeMap = new Pipe[noOfXPipes, noOfYPipes];

            for (int x = 0; x < noOfXPipes; x++)
                for (int y = 0; y < noOfYPipes; y++)
                {
                    PipeMap[x, y] = new Pipe(pipeMap[x][y], x, y, noOfXPipes, noOfYPipes);
                    if (PipeMap[x, y].IsStartLocation)
                        StartLocation = PipeMap[x, y];
                }

            if (StartLocation == null)
                throw new ArgumentException("No starting location fond");
        }

        public long MoveToYouMeetAnimal()
        {
            long moveCounter = 0;

            var isOnSameLocation = false;
            var animalLocation = StartLocation;
            var yourLocation = StartLocation;

            while(!isOnSameLocation)
            {

            }


            return moveCounter;
        }
    }
}
