using NUnit.Framework;
using Assert = UniDi.Internal.Assert;

namespace UniDi.Tests.Bindings
{
    [TestFixture]
    public class TestNonLazy : UniDiUnitTestFixture
    {
        [Test]
        public void Test1()
        {
            Container.Bind<Foo>().AsSingle().NonLazy();

            Assert.Throws(() => Container.ResolveRoots());
        }

        [Test]
        public void Test2()
        {
            Container.Bind<Foo>().AsSingle();

            Container.ResolveRoots();
        }

        [Test]
        public void Test3()
        {
            Container.Bind<Foo>().AsSingle().NonLazy();
            Container.Bind<Bar>().AsSingle();

            Container.ResolveRoots();
        }

        public class Foo
        {
            public Foo(Bar bar)
            {
            }
        }

        public class Bar
        {
        }
    }
}
