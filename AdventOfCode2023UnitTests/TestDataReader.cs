﻿using Common;

namespace AdventOfCode2023UnitTests
{
    internal static class TestDataReader
    {
        internal static string[] ReadDataSet(string fileName)
        {
            return TestDataRepository.ReadDataSet(fileName);
        }
    }
}
