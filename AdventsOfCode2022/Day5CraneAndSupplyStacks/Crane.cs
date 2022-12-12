namespace AdventsOfCode2022.Day5CraneAndSupplyStacks
{
    internal class Crane
    {
        internal Queue<CraneCommand> CommandList { get; private set; }
        internal Ship ShipReference { get; private set; }

        internal Crane(Ship shipRef)
        {
            ShipReference = shipRef;
            CommandList = new Queue<CraneCommand>();
        }

        /// <summary>
        /// This is how the input lines will look like
        /// 
        /// ... some irrelevant things...
        ///
        ///move 1 from 2 to 1
        ///move 3 from 1 to 3
        ///move 2 from 2 to 1
        ///move 1 from 1 to 2
        ///
        /// </summary>
        /// <param name="datasetLines"></param>
        /// <exception cref="Exception"></exception>
        internal void LoadCommands(string[] datasetLines)
        {
            if (CommandList == null)
                throw new Exception("Crane.LoadCommands: CommandList queue is null");

            foreach (var line in datasetLines)
            {
                if (line.StartsWith("move"))
                {
                    var linePieces = line.Split(" ");

                    var repeats = int.Parse(linePieces[1]);
                    var from = int.Parse(linePieces[3]);
                    var to = int.Parse(linePieces[5]);

                    for(int r = 0; r<repeats;r++)
                    {
                        CommandList.Enqueue(new CraneCommand(from, to));
                    }
                }
            }
        }

        internal void ExecuteCommands()
        {
            int commandNo = 0;
            while(CommandList.Any())
            {
                commandNo++;

                var command = CommandList.Dequeue();
                if (!ShipReference.Stacks[command.MoveFrom - 1].Any())
                    throw new Exception($"Exception: Crane.ExecuteCommands: Command {commandNo}: Attempt to move crate from stack {command.MoveFrom} but it was empty");

                var crate = ShipReference.Stacks[command.MoveFrom - 1].Pop();
                ShipReference.Stacks[command.MoveTo - 1].Push(crate);
            }
        }
    }
}