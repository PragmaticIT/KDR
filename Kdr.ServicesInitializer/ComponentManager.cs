using Kdr.Database.LiteDb;
using Kdr.ServiceInterfaces;
using Kdr.ServiceInterfaces.Repositories;
using Kdr.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdr.ServicesInitializer
{
    public sealed class ComponentManager
    {
        private static readonly ComponentManager _componentManager = new ComponentManager();
        private static readonly UnityContainer _container;

        static ComponentManager() {
            _container = new UnityContainer();
            RegisterTypes();
        }

        private ComponentManager() { }

        private static void RegisterTypes()
        {
            _container.RegisterType<IAuthService, AuthService>();
            _container.RegisterType<IHashingService, HashingService>();
            _container.RegisterType<ICategoryService, CategoryService>();
            _container.RegisterType<ICategoryRepository, CategoryRepository>();
        }

        public static object GetInstance(Type type)
        {
            return _container.Resolve(type);
        }

        public static T GetInstance<T>()
        {
            return _container.Resolve<T>(); 
        }
    }

}
