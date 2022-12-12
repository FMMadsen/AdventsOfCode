namespace AdventsOfCode2022.Day5CraneAndSupplyStacks
{
    internal class Crane
    {
        internal Queue<CraneCommand> CommandList { get; private set; }
        internal Ship ShipReference { get; private set; }
        internal bool AbilityToMoveMultipleCratesSimultaniously { get; set; }

        internal Crane(Ship shipRef)
        {
            ShipReference = shipRef;
            CommandList = new Queue<CraneCommand>();
            AbilityToMoveMultipleCratesSimultaniously = false;
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
                    CommandList.Enqueue(new CraneCommand(from, to, repeats));
                }
            }
        }

        internal void ExecuteCommands()
        {
            if (AbilityToMoveMultipleCratesSimultaniously)
                ExecuteCommandsMultipleBoxesAtATime();
            else
                ExecuteCommandsOneBoxAtATime();
        }

        internal void ExecuteCommandsOneBoxAtATime()
        {
            int commandNo = 0;
            while (CommandList.Any())
            {
                commandNo++;

                var command = CommandList.Dequeue();
                if (!ShipReference.Stacks[command.MoveFrom - 1].Any())
                    throw new Exception($"Exception: Crane.ExecuteCommands: Command {commandNo}: Attempt to move crate from stack {command.MoveFrom} but it was empty");

                for(int i = 0; i < command.Repeats; i++)
                {
                    var crate = ShipReference.Stacks[command.MoveFrom - 1].Pop();
                    ShipReference.Stacks[command.MoveTo - 1].Push(crate);
                }
            }
        }

        internal void ExecuteCommandsMultipleBoxesAtATime()
        {
            int commandNo = 0;
            while (CommandList.Any())
            {
                commandNo++;

                var command = CommandList.Dequeue();
                if (!ShipReference.Stacks[command.MoveFrom - 1].Any())
                    throw new Exception($"Exception: Crane.ExecuteCommands: Command {commandNo}: Attempt to move crate from stack {command.MoveFrom} but it was empty");

                if (command.Repeats == 1)
                {
                    var crate = ShipReference.Stacks[command.MoveFrom - 1].Pop();
                    ShipReference.Stacks[command.MoveTo - 1].Push(crate);
                }
                else if (command.Repeats > 1)
                {
                    //Load all crates to the crane carrier
                    var crateCarrier = new char[command.Repeats];
                    for (int i = command.Repeats-1; i > -1; i--)
                    {
                        crateCarrier[i] = ShipReference.Stacks[command.MoveFrom - 1].Pop();
                    }
                    //Unload all crates to the ship in same order
                    for (int i = 0; i < command.Repeats; i++)
                    {
                        ShipReference.Stacks[command.MoveTo - 1].Push(crateCarrier[i]);
                    }
                }
            }
        }
    }
}