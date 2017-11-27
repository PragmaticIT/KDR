using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kdr.ServiceInterfaces.Repositories;
using Kdr.Core;

namespace Kdr.Services.Tests
{
    /// <summary>
    /// Summary description for CategoryServiceTests
    /// </summary>
    [TestClass]
    public class CategoryServiceTests
    {
        [TestMethod]
        public void CanCreateCategory()
        {
            var sut = new CategoryService(new CategoryRepositoryFake());
            var result = sut.CreateCategory(new ServiceInterfaces.CreateCategoryInput { Name = "Jedzenie" });
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Category);
            Assert.AreEqual(100, result.Category.Id);
            Assert.AreEqual("New category: Jedzenie", result.Category.Name);
        }
    }

    internal class CategoryRepositoryFake : ICategoryRepository
    {
        public bool Delete(Kdr.Core.Category input)
        {
            if(input == null)
            {
                throw new ArgumentNullException();
            }
            return Delete(input.Id);
        }

        public bool Delete(int id)
        {
            return (id % 2 == 0);
        }

        public Kdr.Core.Category Get(int id)
        {
            if(id % 2 != 0)
            {
                throw new ArgumentOutOfRangeException("No category with such id.");
                //return null;
            }
            return new Core.Category { Id = id, Name = "Category " + id };

        }

        public Category FindByName(string name)
        {
            if ("Abc" == name) return new Category { Name = name, Id = 101 };
            return null;
        }

        public GetPageOutput GetPage(GetPageInput input)
        {
            throw new NotImplementedException();
        }

        public Kdr.Core.Category Save(Kdr.Core.Category input)
        {
            //Null
            if (input == null) throw new ArgumentNullException();

            //Null or white space as Name
            if (String.IsNullOrWhiteSpace(input.Name)) throw new Exception();

            //Try to insert existing name in DB
            if (input.Id == 0 && input.Name == "Abc") throw new Exception(); 

            //New category
            if (input.Id == 0 && !String.IsNullOrWhiteSpace(input.Name))
                return new Core.Category { Id = 100, Name = "New category: " + input.Name };

            //Saved category
            return new Core.Category { Id = input.Id, Name = "Saved category: " + input.Name }; ;

        }
    }
}
