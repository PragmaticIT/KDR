using LiteDB;
using LiteDbTests.TestClasses;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace LiteDbTests
{
    [TestFixture]
    public class InsertTests:LiteDbTestBase
    {
        const string collection1Name = "i_insert_simple";
        const string collection2Name = "i_insert_complex";
        
        protected LiteCollection<SimpleClass> Collection1 { get { return Database.GetCollection<SimpleClass>(collection1Name); } }

        protected LiteCollection<ComplexClass> Collection2 { get { return Database.GetCollection<ComplexClass>(collection2Name); } }

        [TearDown]
        public void TearDown() {
            Database.DropCollection(collection1Name);
            Database.DropCollection(collection2Name);
        }

        [Test]
        public void CanInsertSimpleItem()
        {
            var sut = new SimpleClass { Name = "Jan", Surname = "Kowalski", City = "Krakow" };

            var count = Collection1.Count();

            Collection1.Insert(sut);

            Assert.Greater(Collection1.Count(), count);

            var stored=Collection1.Find(x => x.Name == sut.Name && x.Surname == sut.Surname && x.City == sut.City).ToList();
            Assert.AreEqual(1, stored.Count);
            //Assert.AreSame(sut, stored[0]);
            Assert.AreEqual(sut, stored[0]);
        }

        [Test]
        public void CanInsertCollectionOfSimpleItems() {
            var items = Enumerable.Range(1, 100).Select(i =>
               new SimpleClass { Id=i, Name = "Jan", Surname = "Nowak", City = "Poznan" });
            Collection1.InsertBulk(items);
        }
        [Test]
        public void CanInsertComplexItem()
        {
            var item = new ComplexClass
            {
                Person1 = new SimpleClass { Name = "Maria", Surname = "Kowalska", City = "Krakow" },
                Person2 = new SimpleClass { Name = "Jan", Surname = "Kowalski", City = "Warszawa" },
                Children = new List<SimpleClass> {
                    new SimpleClass{ Name="Krzysztof", Surname="Kowalski", City="Krakow"},
                    new SimpleClass{ Name="Waldemar",Surname="Nowak",City="Brzesko"},
                    new SimpleClass{ Name="Monika",Surname="Kowalska",City="Olsztyn"}
                },
                Remarks = "this record is for test purposes only"
            };
            var count = Collection2.Count();

            Collection2.Insert(item);
            Assert.Greater(Collection2.Count(), count);

            var stored = Collection2.Find(x => x.Person1.Name==item.Person1.Name).ToList();
            Assert.AreEqual(1, stored.Count);
            //Assert.AreSame(item, stored[0]); //todo: WAZNE!!!
            Assert.AreEqual(item, stored[0]);
        }

        [Test]
        public void CanInsertComplexItemToSimpleCollection()
        {
            var item = new ComplexClass
            {
                Person1 = new SimpleClass { Name = "Maria", Surname = "Kowalska", City = "Krakow" },
                Person2 = new SimpleClass { Name = "Jan", Surname = "Kowalski", City = "Warszawa" },
                Children = new List<SimpleClass> {
                    new SimpleClass{ Name="Krzysztof", Surname="Kowalski", City="Krakow"},
                    new SimpleClass{ Name="Waldemar",Surname="Nowak",City="Brzesko"},
                    new SimpleClass{ Name="Monika",Surname="Kowalska",City="Olsztyn"}
                },
                Remarks = "this record is for test purposes only"
            };

            var sut = Database.GetCollection<ComplexClass>(collection1Name);
            sut.Insert(item);
        }
        [Test]
        public void CanInsertCollectionOfComplexItems() {
            var items = Enumerable.Range(1, 100).Select(i => new ComplexClass
            {
                Id=i,
                Person1 = new SimpleClass { Name = "Maria" + i, Surname = "Kowalska" + i, City = "Krakow" + i },
                Person2 = new SimpleClass { Name = "Jan" + i, Surname = "Kowalski" + i, City = "Warszawa" + i },
                Children = new List<SimpleClass> {
                    new SimpleClass{ Name="Krzysztof"+i, Surname="Kowalski"+i, City="Krakow"+i},
                    new SimpleClass{ Name="Waldemar"+i,Surname="Nowak"+i,City="Brzesko"+i},
                    new SimpleClass{ Name="Monika"+i,Surname="Kowalska"+i,City="Olsztyn"+i}
                },
                Remarks = "this record is for test purposes only. Record number: " + i
            });
            var count = Collection2.Count();
            Collection2.InsertBulk(items);
            Assert.AreEqual(100 + count, Collection2.Count());
        }
    }
}
