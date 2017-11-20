using Kdr.Core;

namespace Kdr.ServiceInterfaces
{
    public class CreateCategoryOutput
    {
        private readonly Category _category;

        public CreateCategoryOutput(Category category)
        {
            _category = category;
        }
    }
}