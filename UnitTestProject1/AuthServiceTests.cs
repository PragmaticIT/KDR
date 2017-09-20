using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;
using Kdr.Services;

namespace Kdr.Services.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public void CanValidateUser()
        {
            var hashingService = new HashingService();
            var sut = new Kdr.Services.AuthService(hashingService);
            var result = sut.Validate(new ServiceInterfaces.ValidateInput { Login = "jan", Password = "abc" });

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
