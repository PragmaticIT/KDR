using MongoDB.Driver;
using MongoTests.TestClasses;
using NUnit.Framework;
using System.Linq;

namespace MongoTests
{
    [TestFixture]
    public class UpdateTests : MongoTestBase
    {
        const string collectionName = "u-update";
        protected IMongoCollection<SimpleClass> Collection
        {
            get { return Database.GetCollection<SimpleClass>(collectionName); }
        }
        [TearDown]
        public override void TearDown()
        {
            Database.DropCollection(collectionName);
            base.TearDown();
        }

        [Test]
        public void CanUpdateDocument()
        {
            var doc1 = new SimpleClass { Id = 100, Name = "Jan", Surname = "Kowalski", City = "Krakow" };
            var doc2 = new SimpleClass { Id = 101, Name = "Maria", Surname = "Nowak", City = "Krakow" };

            Collection.InsertMany(new SimpleClass[] { doc1, doc2 });

            var filter = Builders<SimpleClass>.Filter.Eq(x => x.Id, 100);
            var change = Builders<SimpleClass>.Update.Set(x => x.City, "Warszawa");

            Collection.UpdateOne(filter, change);

            var result=Collection.Find(filter).FirstOrDefault();
            Assert.AreEqual("Warszawa", result.City);
        }

        [Test]
        public void CanReplaceDocument() {
            var doc1 = new SimpleClass { Id = 200, Name = "Jan", Surname = "Kowalski", City = "Krakow" };
            var doc2 = new SimpleClass { Id = 200, Name = "Maria", Surname = "Nowak", City = "Krakow" };

            Collection.InsertMany(new SimpleClass[] { doc1 });

            var filter = Builders<SimpleClass>.Filter.Eq(x => x.Id, 200);

            Collection.ReplaceOne(filter, doc2);

            var result = Collection.Find(filter).FirstOrDefault();
            Assert.IsTrue(doc2.Equals(result));
        }
        [Test]
        public void CanUpdateMultipleDocuments() {
            var items = Enumerable.Range(1, 100).Select(i =>
               new SimpleClass { Id=1000+i, Name = "Jan", Surname = "Nowak", City = "Poznan" });
            Collection.InsertMany(items);

            var filter = Builders<SimpleClass>.Filter.And(
                new ExpressionFilterDefinition<SimpleClass>(x => x.Id > 1000),
                new ExpressionFilterDefinition<SimpleClass>(x => x.Id < 1020)
                );

            var change = Builders<SimpleClass>.Update.Set(x => x.City, "Warszawa");

            Collection.UpdateMany(filter, change);

            var result=Collection.Find(new ExpressionFilterDefinition<SimpleClass>(x => x.Id >= 1000 && x.Id <= 1100)).ToList();
            Assert.IsTrue(result.Where(x => x.Id > 1000 && x.Id < 1020).All(x => x.City == "Warszawa"));
            Assert.IsTrue(result.Where(x => x.Id >= 1020 && x.Id <= 1100).All(x => x.City == "Poznan"));

        }
    }
}
