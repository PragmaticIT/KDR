using System.Collections.Generic;
using Kdr.Core;

namespace Kdr.ServiceInterfaces
{
    public class GetAllCategoriesOutput
    {
        public GetAllCategoriesOutput(IEnumerable<Category> categories)
        {
            Categories = categories;
        }

        public IEnumerable<Category> Categories { get; private set; }
    }
}