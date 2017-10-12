using LiteDB;
using NUnit.Framework;
using System.Linq;

namespace LiteDbTests
{
    /// <summary>
    /// <see cref="http://codingcanvas.com/using-mongodb-_id-field-with-c-pocos/"/>
    /// </summary>
    [TestFixture]
    public class InsertSelectTests : LiteDbTestBase
    {
        const string collectionName = "insert_select_tests";
        protected LiteCollection<TestClass> Collection
        {
            get { return Database.GetCollection<TestClass>(collectionName); }
        }

        [Test]
        public void CanGetIdOfInsertedItem()
        {
            // BsonMapper.Global.Entity<TestClass>().Id(x => x.Id, true);

            //var sut = new SimpleClass { Name = "Jan", Surname = "Kowalski", City = "Krakow" };
            var item1 = new TestClass { Name = "xxxx" };
            var item2 = new TestClass { Name = "zzzz" };
            Collection.Insert(item1);
            Collection.Insert(item2);
            Assert.Greater(item1.Id, 0); //TODO: WAZNE! - Id obiektu jest aktualizowane!!!
            Assert.Greater(item2.Id, 0);

            var sut = Collection.FindAll().ToList();
            Assert.IsTrue(sut.All(x => x.Id > 0));
        }

        public class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
