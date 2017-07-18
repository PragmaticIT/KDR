using System;
using System.Collections.Generic;
using System.Text;

namespace Kdr.ServiceInterfaces
{
    public interface IHashingService
    {
        string HashPassword(string password);
    }
}
