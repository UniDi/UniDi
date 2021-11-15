using NUnit.Framework;
using Assert = UniDi.Internal.Assert;

namespace UniDi.Tests.AbstractFactory
{
    [TestFixture]
    public class TestFactory : UniDiUnitTestFixture
    {
        [Test]
        public void TestToSelf()
        {
            Container.BindFactory<Foo, Foo.Factory>().NonLazy();

            Assert.IsNotNull(Container.Resolve<Foo.Factory>().Create());
        }

        public interface IFoo
        {
        }

        public class Foo : IFoo
        {
            public class Factory : PlaceholderFactory<Foo>
            {
            }
        }
    }
}


