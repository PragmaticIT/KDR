using Kdr.Core;

namespace Kdr.ServiceInterfaces
{
    public class GetCategoryOutput : CreateCategoryOutput
    {
        public GetCategoryOutput() : base()
        {

        }

        public GetCategoryOutput(string status) : base(status)
        {

        }

        public GetCategoryOutput(Category category) : base(category)
        {

        }
    }
}