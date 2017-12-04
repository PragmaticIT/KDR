using Kdr.Core;

namespace Kdr.ServiceInterfaces
{
    public class EditCategoryOutput : CreateCategoryOutput
    {
        public EditCategoryOutput()
        {

        }

        public EditCategoryOutput(Category category) : base(category)
        {
        }

        public EditCategoryOutput(string status) : base(status)
        {
        }
    }
}