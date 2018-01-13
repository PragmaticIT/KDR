using Kdr.Core;
using Kdr.ServiceInterfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdr.Database.LiteDb
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IRepositoryConfig config) : base(config)
        {
        }

        public Category FindByName(string name)
        {
             return _repo.FirstOrDefault<Category>(x => x.Name == name);
        }
    }
}
