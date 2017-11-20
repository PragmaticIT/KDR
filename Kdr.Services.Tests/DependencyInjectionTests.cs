using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Kdr.ServiceInterfaces;

namespace Kdr.Services.Tests
{
    [TestClass]
    public class DependencyInjectionTests
    {
        [TestMethod]
        public void CanNotResolveAuthserviceWithoutHashing()
        {
            var container = new UnityContainer();
            container.RegisterType<IHashingService, HashingService>();
            container.RegisterType<IAuthService, AuthService>();
            var sut = container.Resolve<IAuthService>();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void Cokolwiek()
        {
            var sut = new AuthService(new HashingService());
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void Cokolwiek2()
        {
            var container = new UnityContainer();
            container.RegisterType<IRepository, Repository>();
            var sut = container.Resolve<IRepository>();
            var sut2 = container.Resolve<IRepository>();
            Assert.AreNotEqual(sut.Id, sut2.Id);
            
        }

        [TestMethod]
        public void Cokolwiek3()
        {
            var container = new UnityContainer();
            //Singleton w ramach jednego wątku
            container.RegisterType<IRepository, Repository>(new PerThreadLifetimeManager());
            var sut = container.Resolve<IRepository>();
            var sut2 = container.Resolve<IRepository>();
            Assert.AreEqual(sut.Id, sut2.Id);
        }

        [TestMethod]
        public void Cokolwiek4()
        {
            var container = new UnityContainer();
            //Singleton w ramach aplikacji (cyklu życia kontenera) - typ tworzony
            container.RegisterType<IRepository, Repository>(new ContainerControlledLifetimeManager());
            var sut = container.Resolve<IRepository>();
            var sut2 = container.Resolve<IRepository>();
            Assert.AreEqual(sut.Id, sut2.Id);
        }

        [TestMethod]
        public void Cokolwiek5()
        {
            var container = new UnityContainer();
            var instance = new Repository();
            //Singleton w ramach aplikacji (cyklu życia kontenera) - używa przekazanej mu instancji
            container.RegisterInstance<IRepository>(instance);
            var sut = container.Resolve<IRepository>();
            var sut2 = container.Resolve<IRepository>();
            Assert.AreEqual(sut.Id, sut2.Id);
            Assert.AreEqual((instance as IRepository2).Id, sut.Id);
            Assert.AreEqual(((IRepository2)instance).Id, sut.Id);
        }

        [TestMethod]
        public void Cokolwiek6()
        {
            var container = new UnityContainer();
            var sut = container.Resolve<IRepository>();
        }
    }

    public interface IRepository
    {
        string Id { get; }
    }

    public interface IRepository2
    {
        string Id { get; }
    }

    public class Repository : IRepository, IRepository2
    {
        private readonly string _id;
        private readonly string _id2;

        public Repository()
        {
            _id = Guid.NewGuid().ToString();
            _id2 = Guid.NewGuid().ToString();
        }

        string IRepository.Id
        {
            get
            {
                return _id;
            }
        }

        string IRepository2.Id
        {
            get
            {
                return _id2;
            }
        }
    }
}
