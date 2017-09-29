using MongoDB.Driver;
using MongoTests.TestClasses;
using NUnit.Framework;
using System.Linq;

namespace MongoTests
{
    [TestFixture]
    public class SearchTests : MongoTestBase
    {
        const string collectionName = "s-search-tests";
        protected IMongoCollection<SimpleClass> Collection
        {
            get { return Database.GetCollection<SimpleClass>(collectionName); }
        }
        [SetUp]
        public override void Init()
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
            Collection.InsertMany(items);
        }
        [TearDown]
        public override void TearDown()
        {
            Database.DropCollection(collectionName);
            base.TearDown();
        }

        [Test]
        public void CanQueryByExactName()
        {
            var sut1a = Collection.Find(new ExpressionFilterDefinition<SimpleClass>(x => x.Name == "Jan")).ToList();

            var sut1b = Collection.Find(x => x.Name == "Jan").ToList();

            Assert.AreEqual(sut1a, sut1b);
            Assert.AreEqual(50, sut1a.Count);
            Assert.AreEqual("Jan", sut1a[30].Name);
        }
        [Test]
        public void IsSearchCaseSensitive() {
            var sut1a = Collection.Find(new ExpressionFilterDefinition<SimpleClass>(x => x.Name == "Jan")).ToList();

            var sut1c = Collection.Find(x => x.Name == "jan").ToList();

            Assert.AreNotEqual(sut1a, sut1c);
            Assert.AreEqual(50, sut1a.Count);
            Assert.AreEqual(0, sut1c.Count);
        }

        [Test]
        public void CanSearchCaseInsensitive()
        {
            var sut1a = Collection.Find(new ExpressionFilterDefinition<SimpleClass>(x => x.Name == "Jan")).ToList();

            var sut1c = Collection.Find(x => x.Name.ToLower() == "jan").ToList();

            Assert.AreEqual(sut1a, sut1c);
            Assert.AreEqual(50, sut1a.Count);
            Assert.AreEqual(50, sut1c.Count);
            Assert.AreEqual("Jan", sut1c[30].Name);
        }
        [Test]
        public void CanQueryByStringStartingWith()
        {
            var sut2a = Collection.Find(new ExpressionFilterDefinition<SimpleClass>(x => x.Surname.StartsWith("Nowak1"))).ToList();

            var sut2b = Collection.Find(x => x.Surname.StartsWith("Nowak1")).ToList();

            Assert.AreEqual(sut2a, sut2b);
            Assert.AreEqual(12, sut2a.Count);
        }
        [Test]
        public void CanPaginate()
        {
            var sut3a = Collection.Find(new ExpressionFilterDefinition<SimpleClass>(x => x.City == "Krakow")).Limit(10).Skip(10).ToList();

            var sut3b = Collection.Find(x => x.City == "Krakow").Limit(10).Skip(10).ToList();

            Assert.AreEqual(sut3a, sut3b);
            Assert.AreEqual(10, sut3a.Count);
        }

        [Test]
        public void CanSort() {
            var sut = Collection.Find(x => x.City == "Poznan").SortByDescending(x => x.Id).ToList();
            Assert.IsNotEmpty(sut);
            Assert.AreEqual(33, sut.Count);
            Assert.IsTrue(sut[0].Id > sut[1].Id);

            Assert.AreEqual(sut.Select(x => x.Id).ToList(), sut.Select(x => x.Id).OrderByDescending(x => x).ToList());
        }
    }
}
