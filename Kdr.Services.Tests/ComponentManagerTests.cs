using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kdr.ServicesInitializer;
using Kdr.ServiceInterfaces;

namespace Kdr.Services.Tests
{
    [TestClass]
    public class ComponentManagerTests
    {
        [TestMethod]
        public void CanGenIAuthService()
        {
            var sut = ComponentManager.GetInstance<IAuthService>();
            Assert.IsNotNull(sut);
            Assert.IsInstanceOfType(sut, typeof(AuthService));
        }

        [TestMethod]
        public void CanGenIHashingService()
        {
            var sut = ComponentManager.GetInstance<IHashingService>();
            Assert.IsNotNull(sut);
            Assert.IsInstanceOfType(sut, typeof(HashingService));
        }
    }
}
