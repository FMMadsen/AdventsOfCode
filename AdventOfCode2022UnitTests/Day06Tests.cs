namespace AdventOfCode2022UnitTests
{
    [TestFixture]
    public class Day06Tests
    {
        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "7")]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", "5")]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", "6")]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "10")]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "11")]
        public void Part1(string input, string output)
        {
            //Prepare
            var dataset = new string[] { input };
            var solution = new AdventOfCode2022Solutions.Day06.Solution();

            //act
            var result = solution.SolvePart1(dataset);

            //assert
            Assert.That(result, Is.EqualTo(output));
        }

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "19")]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", "23")]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", "23")]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "29")]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "26")]
        public void Part2(string input, string output)
        {
            //Prepare
            var dataset = new string[] { input };
            var solution = new AdventOfCode2022Solutions.Day06.Solution();

            //act
            var result = solution.SolvePart2(dataset);

            //assert
            Assert.That(result, Is.EqualTo(output));
        }
    }
}