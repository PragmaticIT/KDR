using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdr.ServiceInterfaces.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        T Save(T input);
        bool Delete(T input);
        bool Delete(int id);
        GetPageOutput GetPage(GetPageInput input);
#if DEBUG
        IEnumerable<T> GetAll();
#endif
    }
}
