using LiteDB;
using NUnit.Framework;
using System.IO;

namespace LiteDbTests
{
    public abstract class LiteDbTestBase
    {
        private LiteDatabase _database;
        protected LiteDatabase Database { get { return _database; } }

        [OneTimeSetUp]
        //[SetUp]
        public virtual void Init()
        {
            _database = new LiteDatabase(Constants.DbFilePath);
        }
        [OneTimeTearDown]
        //[TearDown]
        public virtual void ClassTearDown()
        {
            _database.Dispose();
            File.Delete(Constants.DbFilePath);
        }
    }
}
