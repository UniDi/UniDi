using System.Linq;
using NUnit.Framework;
using Assert = UniDi.Internal.Assert;

namespace UniDi.Tests.Other
{
    [TestFixture]
    public class TestClearCacheProvider : UniDiUnitTestFixture
    {
        public interface IFoo
        {
        }

        public class Foo1 : IFoo
        {
        }

        public class Foo2 : IFoo
        {
        }

        // For issue https://github.com/modesttree/UniDi/issues/441
        [Test]
        public void Test1()
        {
            Container.Bind<IFoo>().To<Foo1>().AsSingle();

            Assert.That(Container.Resolve<IFoo>() is Foo1);

            var context = new InjectContext(Container, typeof(IFoo));

            var provider = Container.AllProviders.OfType<CachedProvider>()
                .Where(x => x.GetInstanceType(context) == typeof(Foo1)).Single();

            Assert.IsEqual(provider.NumInstances, 1);

            provider.ClearCache();

            Assert.IsEqual(provider.NumInstances, 0);

            Container.Rebind<IFoo>().To<Foo2>().AsSingle();

            Assert.That(Container.Resolve<IFoo>() is Foo2);
        }
    }
}

