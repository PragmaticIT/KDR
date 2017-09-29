using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTests
{
    [TestFixture]
    public class ConnectivityTests
    {
        [Test]
        public void CheckConnection()
        {
            try
            {
                var client = new MongoClient(Constants.MongoConnection);
                var database = client.GetDatabase(Constants.DbName);
                //database.o
                database.RunCommandAsync((Command<BsonDocument>)"{ping:1}")
                    .Wait();
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.Fail("No connection: " + ex.ToString());
            }
        }

        [Test]
        public void CheckConnection2()
        {
            try
            {
                var client = new MongoClient(new MongoClientSettings
                {
                    Credentials = new List<MongoCredential> {
                    new MongoCredential("SCRAM-SHA-1",
                    new MongoInternalIdentity("test-hg", "user"),
                    new PasswordEvidence("password"))
                },
                    Server = new MongoServerAddress("ds151014.mlab.com", 51014)
                });
                var database = client.GetDatabase(Constants.DbName);
                //database.o
                database.RunCommandAsync((Command<BsonDocument>)"{ping:1}")
                    .Wait();
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.Fail("No connection: " + ex.ToString());
            }
        }
    }
}
