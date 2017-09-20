using Kdr.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Kdr.Services
{
    public class HashingService : IHashingService
    {
        public string HashPassword(string password)
        {
            SHA256 hasher = SHA256.Create();
            var result = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Encoding.UTF8.GetString(result);
        }
    }
}