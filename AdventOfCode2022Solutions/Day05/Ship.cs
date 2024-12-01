using System.Text;

namespace AdventsOfCode2022.Day05CraneAndSupplyStacks
{
    internal class Ship
    {
        internal Stack<Char>[] Stacks { get; private set; }
        internal Crane Crane { get; private set; }

        internal Ship(Stack<Char>[] stacks)
        {
            Crane = new Crane(this);
            Stacks = stacks;
        }

        internal void LoadCraneCommands(string[] datasetLines)
        {
            Crane.LoadCommands(datasetLines);
        }

        internal void ExecuteCraneCommands()
        {
            Crane.ExecuteCommands();
        }

        internal string GetTopCrates()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Stacks.Length; i++)
            {
                if (Stacks[i].Any())
                    sb.Append(Stacks[i].Peek());
            }
            return sb.ToString();
        }
    }
}
