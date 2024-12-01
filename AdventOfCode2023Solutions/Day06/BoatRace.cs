namespace AdventOfCode2023Solutions.Day06
{
    /// <summary>
    /// Distance equation: 2nd degree equation: 
    /// TEMPLATE:   axx + bx + c = y
    /// ACTUAL:     -xx + bx = y
    /// 
    /// a = +1                          (constant, discriminant)
    /// b = total race time (TRT)       (constant, discriminant)
    /// c = 0                           (constant, discriminant)
    /// x : button time (BT)            (variable)
    /// y : actual distance raced (ADR) (variable)
    /// 
    /// Distance record equation: 1st degree equation
    /// y : distance record (DR)        (constnat)
    /// 
    /// To find the minimum and maximum of the hold button time, we need to create a
    /// common form of the two, to find the intersections between the two lines
    /// -(BT*BT) + TRT*BT = DR
    /// 
    /// To find IF there are any intersections between the two lines, we need to use the 
    /// formular for the discriminant. Which works only if you get it to a zero form first:
    /// 0 = -(BT*BT) + TRT*BT - DR   (remember the original y=axx + bx + c)
    /// 
    /// If Discriminant are 0: There are exactly 1 solution which essentially means that we can at best
    /// be doing same time as the record. Menas 0 was to beat the record.
    /// 
    /// If Discriminant are negative: There are no solutions (boat can never beat the record)
    /// If Discriminant are positive: There are 2 solutions (minimum and maximum button time) This is our only way.
    /// 
    /// Of course we need to remember that we need to beat the record, so if we intersect directly
    /// in a whole number, it means we need to select next integer, to actually beat the record.
    /// Essentially we will find the integeers (whole numbers) that are between the intersections.
    /// Ex. if intersections are 2,5 and 5,6 then 3-4-5 are solutions. If intersections are 7 and 10, then solutions are 8-9
    /// 
    /// Discriminant formular: d = bb - 4ac
    /// In our case it will be the formular we will use in the code: d = TRT*TRT - (4*DR)
    /// 
    /// We then use the known formular for identifying the two intersections: (Button Hold Times)
    /// x1 = -b - root(d) / 2a
    /// x2 = -b + root(d) / 2a
    /// 
    /// Or if there are only 1 intersection: x = -b / 2a (But we don't care about this option)
    /// 
    /// In our case it will be:
    /// x1 = -TRT + root(d) / -2    <=>  x1 = TRT-root(d) / 2   (two solution formular: lowest)
    /// x2 = -TRT - root(d) / -2    <=>  x2 = TRT+root(d) / 2   (two solution formular: highest)
    /// 
    /// Ref1: https://www.desmos.com/calculator
    /// Ref2: https://www.webmatematik.dk/lektioner/matematik-c/ligninger/andengradsligningen
    /// 
    /// </summary>
    internal class BoatRace
    {
        private static long RaceIDCounter = 0;

        internal long RaceNo { get; set; }
        internal long TotalRaceTime { get; set; }
        internal long DistanceRecord { get; set; }

        public BoatRace(long raceTime, long raceDistanceReccord)
        {
            RaceNo = RaceIDCounter++;
            TotalRaceTime = raceTime;
            DistanceRecord = raceDistanceReccord;
        }

        internal long CalculateNumberOfWinSolutions()
        {
            double discriminant = (TotalRaceTime * TotalRaceTime) - (4 * DistanceRecord);

            if (discriminant > 0)
            {
                double xLow = (TotalRaceTime - Math.Sqrt(discriminant)) / 2;
                long lowestButtonTime = GetNextUpperWholeNumber(xLow);

                double xHigh = (TotalRaceTime + Math.Sqrt(discriminant)) / 2;
                long highestButtonTime = GetNextLowerWholeNumber(xHigh);

                long winSolutions = highestButtonTime - lowestButtonTime + 1;
                return winSolutions;
            }

            return 0;
        }

        private long GetNextUpperWholeNumber(double decimalNumber)
        {
            if (decimalNumber % 1 != 0)
                return (long)Math.Round(decimalNumber, 0, MidpointRounding.ToPositiveInfinity);

            return (long)decimalNumber + 1;
        }

        private long GetNextLowerWholeNumber(double decimalNumber)
        {
            if (decimalNumber % 1 != 0)
                return (long)Math.Round(decimalNumber, 0, MidpointRounding.ToNegativeInfinity);

            return (long)decimalNumber - 1;
        }

        internal long CalcRaceDistanceFromTime(long holdButtonTime)
        {
            return (TotalRaceTime - holdButtonTime) * holdButtonTime;
        }
    }
}