using Kdr.Core;

namespace Kdr.ServiceInterfaces
{
    public class CreateCategoryOutput
    {
        public CreateCategoryOutput()
        {
        }

        public CreateCategoryOutput(Category category)
        {
            Category = category;
            Status = "OK";
        }

        public CreateCategoryOutput(string status)
        {
            Status = status;
        }

        public Category Category { get; private set; }
        public string Status { get; private set; }
    }
}