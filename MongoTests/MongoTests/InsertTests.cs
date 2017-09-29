using MongoTests.TestClasses;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace MongoTests
{
    [TestFixture]
    public class InsertTests:MongoTestBase
    {
        const string collection1Name = "i-insert-simple";
        const string collection2Name = "i-insert-complex";
        const string collectionOtherName = "i-abc";
        
        protected IMongoCollection<SimpleClass> Collection1 { get { return Database.GetCollection<SimpleClass>(collection1Name); } }

        protected IMongoCollection<ComplexClass> Collection2 { get { return Database.GetCollection<ComplexClass>(collection2Name); } }

        public override void TearDown()
        {
            //comment below if you want to analyse results in mongo browser

            Database.DropCollection(collection1Name);
            Database.DropCollection(collection2Name);
            Database.DropCollection(collectionOtherName);

            //
            base.TearDown();
        }

        [Test]
        public void CanInsertSimpleItem()
        {
            var sut = new SimpleClass { Name = "Jan", Surname = "Kowalski", City = "Krakow" };
            Collection1.InsertOne(sut);


        }
        [Test]
        public void CanInsertToNonExistingCollection() {
            var sut=Database.GetCollection<SimpleClass>(collectionOtherName);
            sut.InsertOne(new SimpleClass { Name = "Jan", Surname = "Kowalski", City = "Krakow" });
        }
        [Test]
        public void CanInsertCollectionOfSimpleItems() {
            var items = Enumerable.Range(1, 100).Select(i =>
               new SimpleClass { Id=i, Name = "Jan", Surname = "Nowak", City = "Poznan" });
            Collection1.InsertMany(items);
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

            Collection2.InsertOne(item);
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
            sut.InsertOne(item);
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
            Collection2.InsertMany(items);
        }
    }
}
