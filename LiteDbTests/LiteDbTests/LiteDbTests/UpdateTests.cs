using LiteDbTests.TestClasses;
using NUnit.Framework;
using System.Linq;
using LiteDB;

namespace LiteDbTests
{
    [TestFixture]
    public class UpdateTests : LiteDbTestBase
    {
        const string collectionName = "u_update";
        protected LiteCollection<SimpleClass> Collection
        {
            get { return Database.GetCollection<SimpleClass>(collectionName); }
        }
        [TearDown]
        public void TearDown()
        {
            Database.DropCollection(collectionName);
        }

        [Test]
        public void CanUpdateDocument()
        {
            var doc1 = new SimpleClass { Id = 100, Name = "Jan", Surname = "Kowalski", City = "Krakow" };
            var doc2 = new SimpleClass { Id = 101, Name = "Maria", Surname = "Nowak", City = "Krakow" };

            Collection.InsertBulk(new SimpleClass[] { doc1, doc2 });

            doc1.City = "Warszawa";

            Collection.Update(doc1);

            var sut = Collection.FindById(new BsonValue(100));
            Assert.AreEqual("Warszawa", sut.City);
        }

        [Test]
        public void CanUpsertNewDocument() {
            var doc1 = new SimpleClass { Id = 200, Name = "Jan", Surname = "Kowalski", City = "Krakow" };
            var doc2 = new SimpleClass { Id = 201, Name = "Maria", Surname = "Nowak", City = "Krakow" };

            Collection.Upsert(new SimpleClass[] { doc1, doc2 });

            
            var result = Collection.FindAll().ToList();

            Assert.IsTrue(result.Contains(doc1));
            Assert.IsTrue(result.Contains(doc2));

            Assert.AreEqual(2, result.Count);
        }
        [Test]
        public void CanUpsertExitingDocument()
        {
            var doc1 = new SimpleClass { Id = 200, Name = "Jan", Surname = "Kowalski", City = "Krakow" };
            var doc2 = new SimpleClass { Id = 201, Name = "Maria", Surname = "Nowak", City = "Krakow" };

            Collection.InsertBulk(new SimpleClass[] { doc1, doc2 });

            doc1.Surname = "aaaa";
            doc2.Surname = "aaaa";

            Collection.Upsert(new SimpleClass[] { doc1, doc2 });

            var result = Collection.Find(x => x.Surname == "aaaa").ToList();

            Assert.IsTrue(result.Contains(doc1));
            Assert.IsTrue(result.Contains(doc2));

            Assert.AreEqual(2, result.Count);
        }
    }
}
