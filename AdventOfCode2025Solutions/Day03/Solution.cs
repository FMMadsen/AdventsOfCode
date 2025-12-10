using Common;
using ToolsFramework;

namespace AdventOfCode2025Solutions.Day03
{
    public class Solution : IAOCSolution
    {
        public string PuzzleName => "Day 3: Lobby";

        public string SolvePart1(string[] datasetLines)
        {
            var banks = datasetLines.Select(Bank.FromString).ToArray();
            foreach (var bank in banks)
                bank.SetMaxPowerWithNBatteries(2);

            var sumPower = banks.Sum(b => b.BankPower);
            return sumPower.ToString();
        }

        public string SolvePart2(string[] datasetLines)
        {
            var banks = datasetLines.Select(Bank.FromString).ToArray();
            foreach (var bank in banks)
                bank.SetMaxPowerWithNBatteries(12);

            var sumPower = banks.Sum(b => b.BankPower);
            return sumPower.ToString();
        }
    }

    internal class Bank
    {
        public static Bank FromString(string initializationString)
        {
            var batteryJoltages = NumberTools.StringToIntArray(initializationString);
            var batteries = batteryJoltages.Select(j => new Battery(j)).ToArray();

            return new Bank
            {
                BankString = initializationString,
                Batteries = batteries,
                NumberOfBatteries = batteries.Length,
            };
        }

        public required string BankString { get; init; }

        public required int NumberOfBatteries { get; init; }

        public required Battery[] Batteries { get; init; }

        public string BankPowerString => string.Join("", Batteries.Where(b => b.IsOn).Select(b => b.Joltage.ToString()));

        public long BankPower
        {
            get
            {
                var batteriesOnPower = Batteries.Where(b => b.IsOn).Select(b => b.Joltage.ToString());
                string batteriesOnPowerString = string.Join("", batteriesOnPower);
                return long.Parse(batteriesOnPowerString);
            }
        }

        public void SetMaxPowerWithNBatteries(int n)
        {
            //Locate 1 battery at a time. Since power numbers are concatenated, there is not math involved just yet
            //only searching for largest number. And if there are more, then always take left-most to provide
            //as high as possible chance for locating the next higest number

            //digit means what digit are we now searching for. Zero based and left based
            int leftMostAvailableIndex = 0;
            for (int digit = 0; digit < n; digit++)
            {
                //Search in subset of array, since we cannot use e.g a high 9 in last digit if we search for 2 numbers
                var batteriesSubset = Batteries.Take(new Range(leftMostAvailableIndex, NumberOfBatteries - (n - digit - 1)));
                var higestNumber = batteriesSubset.Max(b => b.Joltage);
                var firstHigestBattery = batteriesSubset.First(b => b.Joltage == higestNumber);
                firstHigestBattery.TurnOn();
                
                var indexOfFirstHigest = Array.IndexOf(Batteries, firstHigestBattery);
                leftMostAvailableIndex = indexOfFirstHigest + 1;
            }
        }
    }

    internal class Battery(long joltage)
    {
        public long Joltage { get; private set; } = joltage;

        public bool IsOn { get; private set; } = false;

        public void TurnOn() => IsOn = true;

        public void TurnOff() => IsOn = false;
    }
}
