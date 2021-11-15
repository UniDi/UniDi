
#if !(UNITY_WSA && ENABLE_DOTNET)

using NUnit.Framework;
using Assert = UniDi.Internal.Assert;

namespace UniDi.Tests.Convention.Names
{
    [TestFixture]
    public class TestConventionNames : UniDiUnitTestFixture
    {
        [Test]
        public void TestWithSuffix()
        {
            Container.Bind<IController>()
                .To(x => x.AllNonAbstractClasses().InNamespace("UniDi.Tests.Convention.Names").WithSuffix("Controller")).AsTransient();

            Assert.That(Container.Resolve<IController>() is FooController);
        }

        [Test]
        public void TestWithPrefix()
        {
            Container.Bind<IController>()
                .To(x => x.AllTypes().InNamespace("UniDi.Tests.Convention.Names").WithPrefix("Controller")).AsTransient();

            Assert.That(Container.Resolve<IController>() is ControllerBar);
        }

        [Test]
        public void TestMatchingRegex()
        {
            Container.Bind<IController>()
                .To(x => x.AllNonAbstractClasses().InNamespace("UniDi.Tests.Convention.Names").MatchingRegex("Controller$")).AsTransient();

            Assert.That(Container.Resolve<IController>() is FooController);
        }

        interface IController
        {
        }

        class FooController : IController
        {
        }

        class ControllerBar : IController
        {
        }

        class QuxControllerAsdf : IController
        {
        }

        class IgnoredFooController
        {
        }

        class ControllerBarIgnored
        {
        }
    }
}

#endif
