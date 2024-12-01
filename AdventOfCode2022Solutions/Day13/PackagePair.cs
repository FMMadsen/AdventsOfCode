namespace AdventsOfCode2022.Day13DistressSignal
{
    internal class PackagePair
    {
        internal Package PackageLeft { get; private set; }
        internal Package PackageRight { get; private set; }

        internal PackagePair(string packageLeftString, string packageRightString)
        {
            PackageLeft = new Package(packageLeftString);
            PackageRight = new Package(packageRightString);
        }
    }
}
