using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kdr.Core.Tests
{
    [TestClass]
    public class RoleTest
    {
        [TestMethod]
        public void CanCheckIsAdmin()
        {
            var sut = Roles.Admin;
            Assert.IsTrue((sut & Roles.Admin) == Roles.Admin);
            Assert.IsFalse((sut & Roles.User) == Roles.User);
            Assert.IsFalse((sut & Roles.SuperAdmin) == Roles.SuperAdmin);
        }

        [TestMethod]
        public void CanCheckIsAdmin2()
        {
            var sut = Roles.Admin | Roles.SuperAdmin;
            Assert.IsTrue((sut & Roles.Admin) == Roles.Admin);
            Assert.IsFalse((sut & Roles.User) == Roles.User);
            Assert.IsTrue((sut & Roles.SuperAdmin) == Roles.SuperAdmin);
        }

        [TestMethod]
        public void CanCheckIsAdmin3()
        {
            var sut = (Roles)3;
            Assert.IsTrue((sut & Roles.Admin) == Roles.Admin);
            Assert.IsFalse((sut & Roles.User) == Roles.User);
            Assert.IsTrue((sut & Roles.SuperAdmin) == Roles.SuperAdmin);
        }
    }
}
