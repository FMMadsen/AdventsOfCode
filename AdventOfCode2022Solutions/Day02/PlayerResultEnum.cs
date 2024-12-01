using System.ComponentModel;

namespace AdventsOfCode2022.Day02RockPaperScissors
{
    public enum PlayerResultEnum
    {
        [Description("Win ")]
        Win,
        [Description("Lose")]
        Lose,
        [Description("Draw")]
        Draw,
    }
}
