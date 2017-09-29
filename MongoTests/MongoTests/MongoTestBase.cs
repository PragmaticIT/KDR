using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTests
{
    public abstract class MongoTestBase
    {
        private MongoClient _client;
        private IMongoDatabase _database;

        protected IMongoDatabase Database { get { return _database; } }

        //[OneTimeSetUp]
        [SetUp]
        public virtual void Init()
        {
            _client = new MongoClient(Constants.MongoConnection);
            _database = _client.GetDatabase(Constants.DbName);
        }
        //[OneTimeTearDown]
        [TearDown]
        public virtual void TearDown()
        {
            _database = null;
            _client = null;
        }
    }
}
