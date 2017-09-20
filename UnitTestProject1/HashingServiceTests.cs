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
        public void HashedPasswordShouldBeEqual()
        {
            var password = "huhuha";
            SHA256 hasher = SHA256.Create();
            var hashed = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            var result = Encoding.UTF8.GetString(hashed);

            var sut = new HashingService();
            Assert.AreEqual(result, sut.HashPassword(password));
        }
    }
}
