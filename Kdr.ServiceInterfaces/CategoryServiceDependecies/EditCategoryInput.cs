using System;

namespace Kdr.ServiceInterfaces
{
    public class EditCategoryInput
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsValid()
        {
            return Id > 0 && !string.IsNullOrWhiteSpace(Name);
        }
    }
}