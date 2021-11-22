
using System.Collections;
using UniDi.Internal;
using UnityEngine.TestTools;
using UniDi.Tests.Installers.Installers;

namespace UniDi.Tests.Installers
{
    public class TestInstallers : UniDiIntegrationTestFixture
    {
        [UnityTest]
        public IEnumerator TestZeroArgs()
        {
            PreInstall();
            FooInstaller.Install(Container);

            PostInstall();

            FixtureUtil.AssertResolveCount<Foo>(Container, 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestOneArg()
        {
            PreInstall();
            BarInstaller.Install(Container, "blurg");

            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "blurg");
            yield break;
        }

        [UnityTest]
        public IEnumerator TestThreeArgs()
        {
            PreInstall();
            QuxInstaller.Install(Container, "blurg", 2.0f, 1);

            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "blurg");
            yield break;
        }
    }
}

