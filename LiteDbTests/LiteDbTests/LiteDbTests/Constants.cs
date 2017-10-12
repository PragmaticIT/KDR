using System.IO;

namespace LiteDbTests
{
    public static class Constants
    {
        public static readonly string DbFilePath =
            Path.Combine(
            Path.GetDirectoryName(typeof(Constants).Assembly.Location),
            "TestDatabase.db");
        public static readonly string RepoFilePath =
            Path.Combine(
            Path.GetDirectoryName(typeof(Constants).Assembly.Location),
            "TestRepository.db");
    }
}
