using System.ComponentModel;

namespace AdventsOfCode2022.Day02RockPaperScissors
{
    public enum ActionEnum
    {
        [Description("Rock")]
        Rock,
        [Description("Papa")]
        Paper,
        [Description("Scis")]
        Scissor,
    }
}
