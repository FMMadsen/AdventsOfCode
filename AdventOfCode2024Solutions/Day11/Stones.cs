using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2024Solutions.Day11
{
    public class Stones
    {
        private List<long> stones = [];
        private int blinkedTimes = 0;

        public Stones(string input)
        {
            stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
        }

        public List<long> Blink(int numberOfBlinks)
        {
            for (int b = 0; b < numberOfBlinks; b++)
            {
                Blink();
            }
            return stones;
        }

        public List<long> Blink()
        {
            var newList = new List<long>();
            foreach (var stone in stones)
            {
                blinkedTimes++;
                DoStoneChangeRule1(stone, newList);
            }
            stones = newList;
            return stones;
        }

        public int Count()
        {
            return stones.Count;
        }

        public override string ToString()
        {
            return string.Join(" ", stones);
        }

        private void DoStoneChangeRule1(long currentStone, List<long> newStoneList)
        {
            if (currentStone == 0)
            {
                newStoneList.Add(1);
                return;
            }
            DoStoneChangeRule2(currentStone, newStoneList);
        }

        private void DoStoneChangeRule2(long currentStone, List<long> newStoneList)
        {
            var numberString = currentStone.ToString();
            var numberStringLength = numberString.Length;
            if (numberStringLength % 2 == 0)
            {
                var newLength = numberStringLength / 2;

                var num1 = numberString.Substring(0, newLength);
                var num2 = numberString.Substring(newLength);
                var long1 = long.Parse(num1);
                var long2 = long.Parse(num2);
                newStoneList.Add(long1);
                newStoneList.Add(long2);
                return;
            }
            DoStoneChangeRule3(currentStone, newStoneList);
        }

        private void DoStoneChangeRule3(long currentStone, List<long> newStoneList)
        {
            newStoneList.Add(currentStone * 2024);
        }

        //private static bool IsEqualNumberOfDigits(ref long number)
        //{
        //    if(FindNumberOfDigit
        //}

        //private static long FindNumberOfDigit(ref long number)
        //{
        //    long count = 0;
        //    while (number > 0)
        //    {
        //        number /= 10;
        //        count++;
        //    }
        //    return count;
        //}
    }
}
