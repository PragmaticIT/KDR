using System;

namespace Kdr.Core
{
    public class User : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }

    [Flags]
    public enum Roles
    {
        User = 4,
        Admin = 1,
        SuperAdmin = 2
    }
}
