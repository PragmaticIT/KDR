using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace Kdr.Database.LiteDb.Tests
{
    /// <summary>
    /// Summary description for CategoryRepositoryTests
    /// </summary>
    [TestClass]
    public class CategoryRepositoryTests
    {
        string _dataBaseLocation;

        [TestInitialize]
        public void Initialize()
        {
            var path = new FileInfo((Assembly.GetExecutingAssembly().Location)).Directory.FullName;
            _dataBaseLocation = Path.Combine(path, "DataBase.db");
            if (File.Exists(_dataBaseLocation)) File.Delete(_dataBaseLocation);
        }

        [TestMethod]
        public void CanFindByName()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
