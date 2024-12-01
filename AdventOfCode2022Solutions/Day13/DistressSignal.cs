namespace AdventsOfCode2022.Day13DistressSignal
{
    internal class DistressSignal
    {
        internal IList<PackagePair> PackagePairs;

        public DistressSignal(string[] distressSignalsPackages)
        {
            PackagePairs = CreatePackagePairs(distressSignalsPackages);
        }

        private IList<PackagePair> CreatePackagePairs(string[] distressSignalsPackages)
        {
            var counter = 0;
            var leftPackage = string.Empty;
            var rightPackage = string.Empty;
            var packagePairList = new List<PackagePair>();

            foreach (var packageString in distressSignalsPackages)
            {
                counter++;

                switch (counter)
                {
                    case 1:
                        leftPackage = packageString;
                        break;
                    case 2:
                        rightPackage = packageString;
                        break;
                    case 3:
                        var packagePair = new PackagePair(leftPackage, rightPackage);
                        packagePairList.Add(packagePair);
                        counter = 0;
                        break;
                }
            }

            return packagePairList;
        }
    }
}