namespace AdventsOfCode2022.Day04CampCleanup
{
    internal class CampSectionRange
    {
        internal string InitialRangeString { get; private set; }
        internal int RangeFrom { get; private set; }
        internal int RangeTo { get; private set; }
        internal int NoOfSectionsAssignedTo { get; private set; }

        internal CampSectionRange(string rangeString)
        {
            InitialRangeString = rangeString;

            var digits = rangeString.Split("-");

            if(digits.Length != 2)
                throw new Exception($"Exception: CampSectionRange: input range string to CampSectionRange constructor not valid: {rangeString}");

            if (int.TryParse(digits[0], out int rangeFrom))
                RangeFrom = rangeFrom;
            else
                throw new Exception($"Exception: CampSectionRange: input range string to CampSectionRange constructor not valid: {rangeString}");

            if (int.TryParse(digits[1], out int rangeTo))
                RangeTo = rangeTo;
            else
                throw new Exception($"Exception: CampSectionRange: input range string to CampSectionRange constructor not valid: {rangeString}");

            NoOfSectionsAssignedTo = RangeTo - RangeFrom + 1;
        }
    }
}
