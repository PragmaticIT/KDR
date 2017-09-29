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
    [TestFixture(Category="Collections")]
    public class CollectionTests:MongoTestBase
    {
        [Test]
        public void CanCreateCollection() {
            var collectionName = "x-testing" + DateTime.Now.Ticks;
            Database.CreateCollection(collectionName);
        }

        [Test]
        public void CanDeleteCollection() {
            var collectionName = "x-testing" + DateTime.Now.Ticks;

            Database.CreateCollection(collectionName);

            Database.DropCollection(collectionName);
        }

        [Test]
        public void CanDeleteNonexistentCollection() {
            var collectionName = "x-testing" + DateTime.Now.Ticks;

            Database.DropCollection(collectionName);

        }

        [Test]
        public void CanQueryForCollection() {
            var collectionName = "x-testing" + DateTime.Now.Ticks;
            Database.CreateCollection(collectionName);

            var sut=Database.GetCollection<object>(collectionName);
            Assert.IsNotNull(sut);
        }

        [Test]
        public void CanGetCollectionList()
        {
            //let's create something just to ensure there is some collection in db
            var collectionName = "x-testing" + DateTime.Now.Ticks;
            Database.CreateCollection(collectionName); 

            List<string> result = //new List<string>();

            Database.ListCollections()
                .ToEnumerable<BsonDocument>()
                .Select(x => x["name"].AsString)
                .ToList();

            //foreach (BsonDocument collection in Database.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            //{
            //    string name = collection["name"].AsString;
            //    result.Add(name);
            //}
            Assert.IsNotEmpty(result);
        }

        [TearDown]
        public override void TearDown()
        {
            var collections = Database.ListCollections()
                .ToEnumerable<BsonDocument>()
                .Select(x => x["name"].AsString)
                .Where(x => x.StartsWith("x-"))
                .ToList();

            collections.ForEach(name => Database.DropCollection(name));

            base.TearDown();
        }
    }
}
