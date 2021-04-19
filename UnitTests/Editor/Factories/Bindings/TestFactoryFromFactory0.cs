using NUnit.Framework;
using Assert = UniDi.Internal.Assert;

namespace UniDi.Tests.Bindings
{
    [TestFixture]
    public class TestFactoryFromFactory0 : UniDiUnitTestFixture
    {
        static Foo StaticFoo = new Foo();

        [Test]
        public void TestSelf()
        {
            Container.BindFactory<Foo, Foo.Factory>().FromIFactory(b => b.To<CustomFooFactory>().AsCached()).NonLazy();

            Assert.IsEqual(Container.Resolve<Foo.Factory>().Create(), StaticFoo);
        }

        [Test]
        public void TestConcrete()
        {
            Container.BindFactory<IFoo, IFooFactory>()
                .To<Foo>().FromIFactory(b => b.To<CustomFooFactory>().AsCached()).NonLazy();

            Assert.IsEqual(Container.Resolve<IFooFactory>().Create(), StaticFoo);
        }

        [Test]
        public void TestFactoryValidation()
        {
            Container.BindFactory<IFoo, IFooFactory>()
                .To<Foo>().FromIFactory(b => b.To<CustomFooFactoryWithValidate>().AsCached()).NonLazy();

            Container.Resolve<IFooFactory>().Create();
        }

        class CustomFooFactoryWithValidate : IFactory<Foo>, IValidatable
        {
            public Foo Create()
            {
                return StaticFoo;
            }

            public void Validate()
            {
                throw Assert.CreateException("Test error");
            }
        }

        class CustomFooFactory : IFactory<Foo>
        {
            public Foo Create()
            {
                return StaticFoo;
            }
        }

        interface IFoo
        {
        }

        class IFooFactory : PlaceholderFactory<IFoo>
        {
        }

        class Foo : IFoo
        {
            public class Factory : PlaceholderFactory<Foo>
            {
            }
        }
    }
}


