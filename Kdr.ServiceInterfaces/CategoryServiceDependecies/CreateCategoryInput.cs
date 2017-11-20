using Kdr.Core;

namespace Kdr.ServiceInterfaces
{
    public class CreateCategoryInput
    {
        public CreateCategoryInput(Category category)
        {
            Category = category;
        }

        public Category Category { get; private set; }
    }
}