using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using Kdr.Core;

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
        }

        [TestMethod]
        public void CanCreateDataBase()
        {
            var sut = new RepositoryBase<Category>(new RepositoryConfig { DbFilePath = _dataBaseLocation });
            sut.GetAll();
            Assert.IsTrue(File.Exists(_dataBaseLocation));
        }
    }
}
