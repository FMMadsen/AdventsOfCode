using AdventOfCode2023Solutions.Day12;

namespace AdventOfCode2023UnitTests
{
    public class Day12Tests
    {
        [Test]
        public void Part1()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2023Solutions.Day12.Solution(dataset);

            //act
            var result = solution.SolvePart1();

            //assert
            Assert.That(result, Is.EqualTo("21"));
        }

        [Test]
        [Ignore("Part 2 not yet implemented")]
        public void Part2()
        {
            //Prepare
            var dataset = TestDataReader.ReadDataSet("TestDataSetDay12.txt");
            var solution = new AdventOfCode2023Solutions.Day12.Solution(dataset);

            //act
            var result = solution.SolvePart2();

            //assert
            Assert.That(result, Is.EqualTo(""));
        }

        [TestCase("#### 4", 1, 1)]
        [TestCase(".#### 4", 1, 1)]
        [TestCase("..#### 4", 1, 1)]
        [TestCase("..???##??.. 3", 2, 2)]
        [TestCase("??##.?? 4", 1, 1)]
        [TestCase("?#?##. 4", 1, 1)]
        [TestCase("#?#?. 4", 1, 1)]
        [TestCase("??#???. 4", 3, 3)]
        [TestCase("???#???. 4", 4, 4)]
        [TestCase("????? 1", 5, 5)]
        public void SpringRow_OneLevelOnly_VariousCases(string input, int expectedNumberOfLevel1GroupStates, int expectedNumberOfPotentialStatesFound)
        {
            //prepare
            SpringRow springRow = new SpringRow(input);

            //act
            springRow.AnalyzeNumberOfPotentialStates();

            //assert
            Assert.That(springRow.NumberOfPotentialStates, Is.EqualTo(expectedNumberOfPotentialStatesFound));
            Assert.That(springRow.PotentialBrokenSpringsGroupsSequenceBegin, Is.Not.Null);
            Assert.That(springRow.PotentialBrokenSpringsGroupsSequenceBegin.Count, Is.EqualTo(expectedNumberOfLevel1GroupStates));
        }

        [TestCase("####.## 4,2", 1)]
        [TestCase("####?## 4,2", 1)]
        [TestCase("??##.## 4,2", 1)]
        [TestCase("#?##.## 4,2", 1)]
        [TestCase("#?#?.## 4,2", 1)]
        public void SpringRow_MultiLevels_SimpleCases(string input, int expectedNumberOfPotentialStatesFound)
        {
            //prepare
            SpringRow springRow = new SpringRow(input);

            //act
            springRow.AnalyzeNumberOfPotentialStates();

            //assert
            Assert.That(springRow.NumberOfPotentialStates, Is.EqualTo(expectedNumberOfPotentialStatesFound));
        }

        [TestCase("..??..??.?? 2,2,2", 1)]
        [TestCase("..??.?? 2,2", 1)]
        [TestCase("##.##.## 2,2,2", 1)]
        [TestCase("##?## 2,2", 1)]
        [TestCase("##?## 2,2", 1)]
        [TestCase("..???????? 2,2,2", 1)]
        [TestCase("?#???.## 4,2", 2)]
        public void SpringRow_MultiLevels_HardCases(string input, int expectedNumberOfPotentialStatesFound)
        {
            //prepare
            SpringRow springRow = new SpringRow(input);

            //act
            springRow.AnalyzeNumberOfPotentialStates();

            //assert
            Assert.That(springRow.NumberOfPotentialStates, Is.EqualTo(expectedNumberOfPotentialStatesFound));
        }

        [TestCase("..#???..## 1,2", 1)]
        [TestCase("..##.....???..## 2,2", 1)]
        [TestCase(".?.??..???..## 2", 1)]
        [TestCase("????.# 1,1", 4)]
        [TestCase("????#.# 2,2,1", 1)]
        [TestCase("????#.# 1,2,1", 2)]
        [TestCase("????#.# 1,1,1", 3)]
        [TestCase("????#.????# 1,2,2,1", 4)]
        public void SpringRow_MultiLevels_HarderCases(string input, int expectedNumberOfPotentialStatesFound)
        {
            //prepare
            SpringRow springRow = new SpringRow(input);

            //act
            springRow.AnalyzeNumberOfPotentialStates();

            //assert
            Assert.That(springRow.NumberOfPotentialStates, Is.EqualTo(expectedNumberOfPotentialStatesFound));
        }

        [TestCase("#.#.### 1,1,3", 1)]
        [TestCase(".#...#....###. 1,1,3", 1)]
        [TestCase(".#.###.#.###### 1,3,1,6", 1)]
        [TestCase("####.#...#... 4,1,1", 1)]
        [TestCase("#....######..#####. 1,6,5", 1)]
        [TestCase(".###.##....# 3,2,1", 1)]
        public void SpringRow_OfficialCases1(string input, int expectedNumberOfPotentialStatesFound)
        {
            //prepare
            SpringRow springRow = new SpringRow(input);

            //act
            springRow.AnalyzeNumberOfPotentialStates();

            //assert
            Assert.That(springRow.NumberOfPotentialStates, Is.EqualTo(expectedNumberOfPotentialStatesFound));
        }
        //[TestCase("????.######..#####. 1,6,5", 4)] //fail

        [TestCase("???.### 1,1,3", 1)]
        [TestCase(".??..??...?##. 1,1,3", 4)]
        [TestCase("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
        [TestCase("????.#...#... 4,1,1", 1)]
        [TestCase("?###???????? 3,2,1", 10)]
        public void SpringRow_OfficialCases2(string input, int expectedNumberOfPotentialStatesFound)
        {
            //prepare
            SpringRow springRow = new SpringRow(input);

            //act
            springRow.AnalyzeNumberOfPotentialStates();

            //assert
            Assert.That(springRow.NumberOfPotentialStates, Is.EqualTo(expectedNumberOfPotentialStatesFound));
        }

        [TestCase("###.", 1, 3, 3, 0)]
        [TestCase("###.", 1, 4, 3, 0)]
        [TestCase("###.", 0, 2, 4, 0)]
        [TestCase("###.", 0, 2, 3, 1)]
        [TestCase("###.", 0, 8, 3, 1)]
        [TestCase("####", 0, 8, 3, 1)]
        [TestCase("#?#.", 0, 2, 3, 1)]
        [TestCase("#?#?", 0, 2, 3, 1)]
        [TestCase("..???##??..", 2, 7, 3, 2)]
        [TestCase("..?#?##??..", 2, 7, 3, 2)]
        public void Test_Analyzer_IdentifyPossibleBrokenSpringsGroupsWithinLimitedArea(string input, int startIndex, int endIndex, int targetSize, int expectedOutput)
        {
            //Prepare
            var springs = input.ToArray();

            //act
            var resultGroups = AdventOfCode2023Solutions.Day12.Analyzer.IdentifyPotentialBrokenSpringsGroupsWithinLimitedArea(null, springs, startIndex, endIndex, targetSize, null);
            var numberOfPossibleBrokenSprings = resultGroups?.Count ?? 0;

            //assert
            Assert.That(numberOfPossibleBrokenSprings, Is.EqualTo(expectedOutput));
        }

        [TestCase("###", 0, 0)]
        [TestCase("?##", 0, 1)]
        [TestCase("#?#", 0, 0)]
        [TestCase("??#", 0, 2)]
        [TestCase("??#", 1, 1)]
        [TestCase("???", 0, 3)]
        [TestCase("???", 1, 2)]
        [TestCase("???", 2, 1)]
        [TestCase("???", 3, 0)]
        [TestCase("???", 4, 0)]
        public void Test_Analyzer_CountNumberOfConsequitiveUnknownSprings(string input, int startIndex, int expectedOutput)
        {
            //Prepare
            var springs = input.ToArray();

            //act
            var result = AdventOfCode2023Solutions.Day12.Analyzer.CountNumberOfBeginsWithConsequitiveUnknownSprings(springs, startIndex);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("###..", 0, 0)]
        [TestCase("?##..", 0, 0)]
        [TestCase("#?#..", 0, 0)]
        [TestCase("??#..", 3, 2)]
        [TestCase("??#..", 1, 0)]
        [TestCase("???..", 0, 0)]
        [TestCase("..???..", 0, 2)]
        [TestCase("???..", 1, 0)]
        [TestCase("???..", 2, 0)]
        [TestCase(".?.?.?.", 0, 1)]
        [TestCase(".?.?.?.", 1, 0)]
        [TestCase("......", 6, 0)]
        [TestCase("...", 0, 3)]
        [TestCase("...", 1, 2)]
        [TestCase("...", 2, 1)]
        [TestCase("...", 3, 0)]
        [TestCase("...", 4, 0)]
        public void Test_Analyzer_CountNumberOfConsequitiveGoodSprings(string input, int startIndex, int expectedOutput)
        {
            //Prepare
            var springs = input.ToArray();

            //act
            var result = AdventOfCode2023Solutions.Day12.Analyzer.CountNumberOfBeginsWithConsequitiveGoodSprings(springs, startIndex);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("#.", 1, true)]
        [TestCase("#.", 0, false)]
        [TestCase("?.", 0, true)]
        [TestCase("..", 0, true)]
        [TestCase("", 0, false)]
        [TestCase(".", 1, false)]
        [TestCase("..", 2, false)]
        public void Test_Analyzer_IsSpringPotentialNonBroken(string input, int startIndex, bool expectedOutput)
        {
            //Prepare
            var springs = input.ToArray();

            //act
            var result = AdventOfCode2023Solutions.Day12.Analyzer.IsSpringPotentialNonBroken(springs, startIndex);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("#.", 1, false)]
        [TestCase("#.", 0, true)]
        [TestCase("?.", 0, true)]
        [TestCase("..", 0, false)]
        [TestCase("..", 1, false)]
        [TestCase(".?", 1, true)]
        [TestCase(".#", 1, true)]
        [TestCase("", 0, false)]
        [TestCase("#", 1, false)]
        [TestCase("#", 2, false)]
        public void Test_Analyzer_IsSpringPotentialBroken(string input, int startIndex, bool expectedOutput)
        {
            //Prepare
            var springs = input.ToArray();

            //act
            var result = AdventOfCode2023Solutions.Day12.Analyzer.IsSpringPotentialBroken(springs, startIndex);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase(0, 2, 3, 1)]
        [TestCase(0, 1, 1, 2)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 4, 2, 3)]
        public void Test_Analyzer_CalculateNumberOfPossibleGroupsWithinRange(int startIndex, int endIndex, int lenght, int expectedOutput)
        {
            //Prepare

            //act
            var result = AdventOfCode2023Solutions.Day12.Analyzer.CalculateNumberOfPotentialGroupsWithinRange(startIndex, endIndex, lenght);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [TestCase("###???..", 0, false)]
        [TestCase("###???..", 1, false)]
        [TestCase("###???..", 2, false)]
        [TestCase("###???..", 3, true)]
        [TestCase("###???...", 4, true)]
        [TestCase("###???..", 5, true)]
        [TestCase("###???..", 6, true)]
        [TestCase("###???..", 7, true)]
        [TestCase("###???..", 8, false)]
        [TestCase("..??##", 0, false)]
        [TestCase("..??##", 2, false)]
        [TestCase("..??##", 3, false)]
        [TestCase("..??##", 4, false)]
        [TestCase("..??##", 5, false)]
        [TestCase("..??##", 6, false)]
        [TestCase("", 6, false)]
        [TestCase(" ", 6, false)]
        public void Test_Analyzer_IsAllRemainingSpringsGood(string input, int startIndex, bool expectedOutput)
        {
            //Prepare
            var springs = input.ToArray();

            //act
            var result = AdventOfCode2023Solutions.Day12.Analyzer.IsAllRemainingSpringsGood(springs, startIndex);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        //[TestCase("..??##", 2, false)]
        //[TestCase("", 3, )]
        //[TestCase("", 4, )]
        //[TestCase("", 5, )]
        //[TestCase("", 6, )]
        //[TestCase("", 6, )]
        [TestCase("#.#.### 1,1,3", 7, 5, 0, 5, 0, 1, 1)]
        [TestCase("?.#.### 1,1,3", 7, 4, 1, 5, 1, 2, 1)]
        [TestCase("??#.### 1,1,3", 7, 4, 2, 5, 1, 4, 2)]
        [TestCase("??#.?###? 1,1,3", 9, 4, 4, 5, 1, 16, 4)]
        [TestCase("???.### 1,1,3", 7, 3, 3, 5, 2, 8, 3)]
        [TestCase("????.### 1,1,3", 8, 3, 4, 5, 2, 16, 6)]
        [TestCase(".??..??...?##. 1,1,3", 14, 2, 5, 5, 3, 32, 10)]
        [TestCase(".??..#?...?##. 1,1,3", 14, 3, 4, 5, 2, 16, 6)]
        public void AlternativeKombiExpander_InitialCalculations(
            string row, 
            int noOfSprings, 
            int noOfBroken, 
            int noOfUnknowns, 
            int maxNoOfBroken,
            int noOfUnknownToBroken,
            int totalNumberOfCombinations,
            int numberOfPotentialCombinations
            )
        {
            //Prepare

            //act
            var analyzer = new AlternativeKombiExpander(row);

            //assert
            Assert.That(analyzer.NumberOfSprings, Is.EqualTo(noOfSprings));
            Assert.That(analyzer.NumberOfBroken, Is.EqualTo(noOfBroken));
            Assert.That(analyzer.NumberOfUnknowns, Is.EqualTo(noOfUnknowns));
            Assert.That(analyzer.MaxNumberOfBroken, Is.EqualTo(maxNoOfBroken));
            Assert.That(analyzer.NumberOfUnknownToBroken, Is.EqualTo(noOfUnknownToBroken));
            Assert.That(analyzer.TotalNumberOfCombinations, Is.EqualTo(totalNumberOfCombinations));
            Assert.That(analyzer.NumberOfPotentialCombinations, Is.EqualTo(numberOfPotentialCombinations));

        }
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 24)]
        [TestCase(5, 120)]
        public void Factorial_tests(long input, long expectedOutput)
        {
            //act
            var result = AlternativeKombiExpander.Factorial(input);

            //assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}