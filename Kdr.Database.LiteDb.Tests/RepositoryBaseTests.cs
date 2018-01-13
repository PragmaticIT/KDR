using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using Kdr.Core;
using System.Linq;
using System.Collections.Generic;

namespace Kdr.Database.LiteDb.Tests
{
    [TestClass]
    public class RepositoryBaseTests
    {
        string _dataBaseLocation;

        [TestInitialize]
        public void Initialize()
        {
            var path = new FileInfo((Assembly.GetExecutingAssembly().Location)).Directory.FullName;
            _dataBaseLocation = Path.Combine(path, "DataBase.db");
            if (File.Exists(_dataBaseLocation)) File.Delete(_dataBaseLocation);
        }

        //[TestCleanup]
        //public void Cleanup()
        //{
        //    if (File.Exists(_dataBaseLocation)) File.Delete(_dataBaseLocation);
        //}

        [TestMethod]
        public void CanCreateDataBase()
        {
            using(var sut = new RepositoryBase<Category>(new RepositoryConfig { DbFilePath = _dataBaseLocation }))
            {
                sut.GetAll();
                Assert.IsTrue(File.Exists(_dataBaseLocation));
            }
            
            
        }

        [TestMethod]
        public void CanSaveCategory()
        {
            using (var sut = new RepositoryBase<Category>(new RepositoryConfig { DbFilePath = _dataBaseLocation }))
            {
                var category = CreateCategory();
                Assert.AreEqual(0, category.Id);
                sut.Save(category);
                var results = sut.GetAll();
                Assert.AreNotEqual(0, category.Id);
                Assert.AreEqual(1, results.Count());
                Assert.AreEqual(category, results.First());
                Assert.AreNotSame(category, results.First());
            }
        }

        [TestMethod]
        public void CanUpdateCategory()
        {
            using (var sut = new RepositoryBase<Category>(new RepositoryConfig { DbFilePath = _dataBaseLocation }))
            {
                var category = CreateCategory();
                sut.Save(category);
                category = sut.Get(category.Id);
                category.Name = "Abc";
                sut.Save(category);
                var results = sut.GetAll();
                Assert.AreEqual(1, results.Count());
                Assert.AreEqual("Abc", results.First().Name);
            }
        }

        [TestMethod]
        public void CanDeleteCategoryById()
        {
            using (var sut = new RepositoryBase<Category>(new RepositoryConfig { DbFilePath = _dataBaseLocation }))
            {
                var category = CreateCategory();
                sut.Save(category);
                sut.Delete(category.Id);
                var results = sut.GetAll();
                Assert.AreEqual(0, results.Count());
            }
        }

        [TestMethod]
        public void CanDeleteCategory()
        {
            using (var sut = new RepositoryBase<Category>(new RepositoryConfig { DbFilePath = _dataBaseLocation }))
            {
                var category = CreateCategory();
                sut.Save(category);
                sut.Delete(sut.Get(category.Id));
                var results = sut.GetAll();
                Assert.AreEqual(0, results.Count());
            }
        }

        [TestMethod]
        public void CanGetCategory()
        {
            using (var sut = new RepositoryBase<Category>(new RepositoryConfig { DbFilePath = _dataBaseLocation }))
            {
                var category1 = CreateCategory();
                sut.Save(category1);
                var category2 = sut.Get(category1.Id);
                Assert.AreEqual(category1, category2);
                Assert.AreNotSame(category1, category2);
            }
        }

        [TestMethod]
        public void CanGetAllCategories()
        {
            using (var sut = new RepositoryBase<Category>(new RepositoryConfig { DbFilePath = _dataBaseLocation }))
            {
                //var categories = new List<Category>();
                //for (int i = 0; i < 10; i++)
                //{
                //    categories.Add(sut.Save(CreateCategory(i)));
                //}
                var categories = Enumerable.Range(0, 9)
                    .Select(x => sut.Save(CreateCategory(x)))
                    .ToList();

                var results = sut.GetAll().ToList();

                CollectionAssert.AreEquivalent(categories, results);
            }
        }

        protected virtual Category CreateCategory(int? number = null)
        {
            return new Category
            { Name =  string.Format("{0} TestCategory {1}", number, DateTime.Now.Ticks).Trim()};
        }
    }
}
