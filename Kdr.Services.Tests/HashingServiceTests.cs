using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;

namespace Kdr.Services.Tests
{
    [TestClass]
    public class HashingServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var password = "huhuha";
            SHA256 hasher = SHA256.Create();
            var hashed = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            var result = Encoding.UTF8.GetString(hashed);

            var sut = new HashingService();
            Assert.AreEqual(result, sut.HashPassword(password));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var sut = new AuthService2();
            sut.HashingService = new HashingService();
            var result = sut.Validate(new ServiceInterfaces.ValidateInput {Login="abc", Password="abc"});
            Assert.AreNotEqual(true, result.IsSuccess);
            
        }
    }
}
