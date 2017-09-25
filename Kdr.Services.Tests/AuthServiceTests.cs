using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;
using Kdr.Services;
using Kdr.ServiceInterfaces;

namespace Kdr.Services.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public void CanValidateUser()
        {
            var hashingService = new HashingService();
            var sut = new AuthService(hashingService);
            var result = sut.Validate(new ValidateInput { Login = "jan", Password = "abc" });

            Assert.IsFalse(result.IsSuccess);
        }
    }
}
