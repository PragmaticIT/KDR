using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Kdr.ServiceInterfaces;

namespace Kdr.Services.Tests
{
    [TestClass]
    public class DependencyInjectionTests
    {
        [TestMethod]
        public void CanNotResolveAuthserviceWithoutHashing()
        {
            var container = new UnityContainer();
            container.RegisterType<IHashingService, HashingService>();
            container.RegisterType<IAuthService, AuthService>();
            var sut = container.Resolve<IAuthService>();
            Assert.IsNotNull(sut);
        }
    }
}
