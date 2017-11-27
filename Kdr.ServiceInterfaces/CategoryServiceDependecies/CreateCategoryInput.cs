using Kdr.Core;

namespace Kdr.ServiceInterfaces
{
    public class CreateCategoryInput
    {
        public bool IsValid()
        {
                return !string.IsNullOrWhiteSpace(Name);
        }

        public string Name { get; set; }
    }
}