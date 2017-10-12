using LiteDbTests.TestClasses;
using NUnit.Framework;
using System;
using System.Linq;

namespace LiteDbTests
{
    [TestFixture(Category="Collections")]
    public class CollectionTests: LiteDbTestBase
    {
        [Test]
        public void CanCreateCollection() {
            var collectionName = "x_testing" + DateTime.Now.Ticks;
            var sut=Database.GetCollection(collectionName);
            Assert.IsNotNull(sut);
        }

        [Test]
        public void CanDeleteCollection() {
            var collectionName = "x_testing" + DateTime.Now.Ticks;

            var sut=Database.GetCollection<SimpleClass>(collectionName);
            sut.Insert(new SimpleClass());
            Assert.IsTrue(Database.CollectionExists(collectionName));

            Database.DropCollection(collectionName);
            Assert.IsFalse(Database.CollectionExists(collectionName));
        }

        [Test]
        public void CanDeleteNonexistentCollection() {
            var collectionName = "x_testing" + DateTime.Now.Ticks;

            Assert.IsFalse(Database.CollectionExists(collectionName));
            Database.DropCollection(collectionName);
            Assert.IsFalse(Database.CollectionExists(collectionName));

        }

        [Test]
        public void CanQueryForCollection() {
            var collectionName = "x_testing" + DateTime.Now.Ticks;

            var sut=Database.GetCollection<object>(collectionName);
            Assert.IsNotNull(sut);
        }

        [Test]
        public void CanGetCollectionList()
        {
            //let's create something just to ensure there is some collection in db
            var collectionName = "x_testing" + DateTime.Now.Ticks;

            var col1=Database.GetCollection<SimpleClass>(collectionName);

            col1.Insert(new SimpleClass()); //TODO: Kolekcja jest zapisywana dopiero jesli zapiszemy w niej obiekt

            var result = Database.GetCollectionNames()
                .ToList();
            
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.IsTrue(result.Contains(collectionName));
        }
        
    }
}
