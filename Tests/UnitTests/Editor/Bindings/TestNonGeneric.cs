using NUnit.Framework;
using Assert = UniDi.Internal.Assert;

namespace UniDi.Tests.Bindings
{
    [TestFixture]
    public class TestNonGeneric : UniDiUnitTestFixture
    {
        [Test]
        public void Test1()
        {
            Assert.Throws(() =>
                Container.Bind(typeof(IFoo), typeof(IBar)).To(typeof(Foo)).AsCached());
        }

        [Test]
        public void Test2()
        {
            Assert.Throws(() =>
                Container.Bind<IFoo>().To(typeof(Bar)).AsCached());
        }

        [Test]
        public void Test3()
        {
            Assert.Throws(() =>
                Container.Bind(typeof(IFoo)).To(typeof(Bar), typeof(Foo)).AsCached());
        }

        [Test]
        public void Test4()
        {
            // This case is more lenient and just ignores invalid bindings
            Container.Bind(typeof(IFoo), typeof(IBar)).To(typeof(Foo), typeof(Bar)).AsCached();

            Assert.IsNotNull(Container.Resolve<IFoo>());
            Assert.IsNotNull(Container.Resolve<IBar>());
        }

        public interface IBar
        {
        }

        public interface IFoo
        {
        }

        public class Bar : IBar
        {
        }

        public class Foo : IFoo
        {
        }
    }
}

