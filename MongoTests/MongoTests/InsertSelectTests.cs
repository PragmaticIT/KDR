using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using NUnit.Framework;

namespace MongoTests
{
    /// <summary>
    /// <see cref="http://codingcanvas.com/using-mongodb-_id-field-with-c-pocos/"/>
    /// </summary>
    [TestFixture]
    public class InsertSelectTests:MongoTestBase
    {
        const string collectionName = "insert-select-tests";
        protected IMongoCollection<TestClass> Collection
        {
            get { return Database.GetCollection<TestClass>(collectionName); }
        }
        public override void TearDown()
        {
            Database.DropCollection(collectionName);
            base.TearDown();
        }
        [Test]
        public void CanGetIdOfInsertedItem() {
            //var sut = new SimpleClass { Name = "Jan", Surname = "Kowalski", City = "Krakow" };
            var sut = new TestClass { Name = "xxxx" };
            Collection.InsertOne(sut);
            Assert.IsFalse(string.IsNullOrWhiteSpace(sut.Id));
        }

        public class TestClass {
            [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
