using System;

namespace Kdr.ServiceInterfaces
{
    public interface IAuthService
    {
        ValidateOutput Validate(ValidateInput input);
    }
}
