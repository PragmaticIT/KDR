using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kdr.Services.Tests
{
    public class ClassA
    {
        
    }

    public class ClassB
    {

    }

    public class ClassC : ClassB
    {

    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(new ClassA() as ClassA);
            Assert.IsNotNull(((ClassA) new ClassA()));
            //Assert.IsNotNull(new ClassA() as ClassB); Error
            //Assert.IsNotNull(((ClassB)new ClassA())); Error
            Assert.IsNotNull(new ClassC() as ClassB);
            Assert.IsNotNull(((ClassB)new ClassC()));
        }

        [TestMethod]
        public void TestMethod2()
        {
            object sut1 = new ClassA();
            object sut2 = new ClassB();
            object sut3 = new ClassC();

            Assert.IsNotNull(sut1 as ClassA);
            Assert.IsNotNull(((ClassA)sut1));
            Assert.IsNull(sut1 as ClassB); //rzutowanie bezpieczne -> zwraca null
            Assert.IsNotNull(((ClassB)sut1)); //rzutowanie niebezpieczne -> exeption
            Assert.IsNotNull(sut3 as ClassB);
            Assert.IsNotNull(((ClassB)sut3));
        }
    }
}
