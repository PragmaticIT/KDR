using Kdr.ServiceInterfaces;
using System;

namespace Kdr.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHashingService _hashingService;

        //Pure evil!
        //public AuthService()
        //{
        //    _hashingService = new HashingService();
        //}

        public AuthService(IHashingService hashingService)
        {
            _hashingService = hashingService;
        }

        public ValidateOutput Validate(ValidateInput input)
        {
            return new ValidateOutput
            {
                IsSuccess = input.Login.ToLower() == "jan"
                        && _hashingService.HashPassword(input.Password) == _hashingService.HashPassword("kowalski")
            };
        }
    }

    //Initialization by Factory pattern
    public static class MyFactory
    {
        public static IHashingService GetHashingService()
        {
            return new HashingService();
        }

        public static IAuthService GetAuthService()
        {
            return new AuthService(GetHashingService());
        }
    }

    //Initialization by property
    public class AuthService2 : IAuthService
    {
        public IHashingService HashingService { get; set; }

        public ValidateOutput Validate(ValidateInput input)
        {
            if(HashingService == null)
            {
                throw new NullReferenceException("Hashing service was not initialized.");
            }
            return new ValidateOutput
            {
                IsSuccess = input.Login.ToLower() == "jan"
                        && HashingService.HashPassword(input.Password) == HashingService.HashPassword("kowalski")
            };
        }
    }
}