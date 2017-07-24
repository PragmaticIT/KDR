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
        }
    }
}
