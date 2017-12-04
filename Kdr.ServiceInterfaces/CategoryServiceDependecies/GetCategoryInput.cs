using System;

namespace Kdr.ServiceInterfaces
{
    public class GetCategoryInput
    {
        public int Id { get; set; }

        public bool IsValid()
        {
            return Id > 0;
        }
    }
}