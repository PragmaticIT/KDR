using LiteDbTests.TestClasses;
using NUnit.Framework;
using System.Linq;
using LiteDB;

namespace LiteDbTests
{
    [TestFixture]
    public class SearchTests : LiteDbTestBase
    {
        const string collectionName = "s_search_tests";
        protected LiteCollection<SimpleClass> Collection
        {
            get { return Database.GetCollection<SimpleClass>(collectionName); }
        }
        [SetUp]
        public void SetUp()
        {
            base.Init();
            var items = Enumerable.Range(1, 100).Select(i =>
               new SimpleClass
               {
                   Id = i,
                   Name = (i % 2 == 0 ? "Jan" : "Maria"),
                   Surname = "Nowak" + i,
                   City = (i % 3 == 0 ? "Poznan" : "Krakow")
               });
            Collection.InsertBulk(items);
        }
        [TearDown]
        public void TearDown()
        {
            Database.DropCollection(collectionName);
        }
        [Test]
        public void CanQueryByExactName()
        {
            var sut1a = Collection.Find(Query.EQ("Name", new BsonValue("Jan"))).ToList();

            var sut1b = Collection.Find(x => x.Name == "Jan").ToList();

            Assert.AreEqual(sut1a, sut1b);
            Assert.AreEqual(50, sut1a.Count);
            Assert.AreEqual("Jan", sut1a[30].Name);
            Assert.AreEqual(sut1a[0], sut1b[0]);
            Assert.AreNotSame(sut1a[0], sut1b[0]);//?!
        }
        [Test]
        public void IsSearchCaseSensitive()
        {
            var sut1a = Collection.Find(Query.EQ("Name", new BsonValue("jan"))).ToList();
            Assert.AreEqual(0, sut1a.Count);

            var sut1b = Collection.Find(x => x.Name == "jan").ToList();
            Assert.AreEqual(0, sut1b.Count);

            //TODO: ERROR! : Property 'Name.ToLower(' was not mapped into BsonDocument.
            //var sut1c = Collection.Find(x => x.Name.ToLower() == "jan").ToList();
            //Assert.AreEqual(50, sut1c.Count);

            var sut2 = Collection.Find(x => x.Name == "Jan").ToList();
            Assert.AreEqual(50, sut2.Count);

            var sut3 = Collection.Find(Query.Contains("Name", "jan")).ToList();
            Assert.AreEqual(0, sut3.Count);

            var sut4 = Collection.Find(Query.StartsWith("Name", "jan")).ToList();
            Assert.AreEqual(0, sut4.Count);

        }

        [Test]
        public void CanQueryByStringStartingWith()
        {
            var sut2a = Collection.Find(Query.StartsWith("Surname", "Nowak1")).ToList();

            var sut2b = Collection.Find(x => x.Surname.StartsWith("Nowak1")).ToList();

            Assert.AreEqual(sut2a, sut2b);
            Assert.AreEqual(12, sut2a.Count);
        }
        [Test]
        public void CanPaginate()
        {
            var sut3a = Collection.Find(Query.EQ("City", new BsonValue("Krakow")), 10, 10).ToList();

            var sut3b = Collection.Find(x => x.City == "Krakow", 10, 10).ToList();

            Assert.AreEqual(sut3a, sut3b);
            Assert.AreEqual(10, sut3a.Count);
        }

        [Test]
        public void CanSort()
        {
            var sut = Collection.Find(Query.All("City", Query.Descending)).ToList();
            Assert.AreEqual("Poznan", sut.First().City);
            Assert.AreEqual("Krakow", sut.Last().City);

        }


        [Test]
        public void CanSortAndFilter()
        {
            var filterQuery = Query.EQ("City", new BsonValue("Poznan"));
            var sortQuery = Query.All(Query.Descending);
            var query = Query.And(sortQuery, filterQuery);

            var sut = Collection.Find(query).ToList();

            Assert.IsNotEmpty(sut);
            Assert.AreEqual(33, sut.Count);
            Assert.IsTrue(sut[0].Id > sut[1].Id);

            Assert.AreEqual(sut.Select(x => x.Id).ToList(), sut.Select(x => x.Id).OrderByDescending(x => x).ToList());
        }
    }
}
