using Kdr.ServiceInterfaces;
using System;

namespace Kdr.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHashingService _hashingService;

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
}
