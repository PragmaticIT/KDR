using Kdr.Core;
using Kdr.ServiceInterfaces.Repositories;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdr.Database.LiteDb
{
    public class RepositoryBase<T> : IDisposable, IRepository<T> where T:IHasId
    {
        protected readonly LiteRepository _repo;

        public RepositoryBase(IRepositoryConfig config)
        {
            _repo = new LiteRepository(config.DbFilePath);
        }

        public bool Delete(T input)
        {
            return _repo.Delete<T>(new BsonValue(input.Id));
        }

        public bool Delete(int id)
        {
            return _repo.Delete<T>(new BsonValue(id));
        }

        public void Dispose()
        {
            if (_repo != null) _repo.Dispose();
        }

        public T Get(int id)
        {
            return _repo.SingleById<T>(new BsonValue(id));
        }

        public IEnumerable<T> GetAll()
        {
            return _repo.Fetch<T>();
        }

        public GetPageOutput GetPage(GetPageInput input)
        {
            throw new NotImplementedException();
        }

        public T Save(T input)
        {
            _repo.Upsert<T>(input);
            return input;
        }
    }
}
