namespace Common.PuzzleRegistration
{
    public sealed record PuzzleDescriptor(
        int Year,
        int Day,
        string ProjectFolderName,
        IAOCSolution Solution);
}
