
using System.Collections;
using UniDi.Internal;
using UnityEngine.TestTools;
using UniDi.Tests.Installers.ScriptableObjectInstallers;

namespace UniDi.Tests.Installers
{
    public class TestScriptableObjectInstallers : UniDiIntegrationTestFixture
    {
        [UnityTest]
        public IEnumerator TestBadResourcePath()
        {
            PreInstall();
            Assert.Throws(() => FooInstaller.InstallFromResource("TestScriptableObjectInstallers/SDFSDFSDF", Container));
            PostInstall();
            yield break;
        }

        [UnityTest]
        public IEnumerator TestZeroArgs()
        {
            PreInstall();
            FooInstaller.InstallFromResource("TestScriptableObjectInstallers/FooInstaller", Container);

            PostInstall();

            FixtureUtil.AssertResolveCount<Foo>(Container, 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestOneArg()
        {
            PreInstall();
            BarInstaller.InstallFromResource("TestScriptableObjectInstallers/BarInstaller", Container, "blurg");

            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "blurg");
            yield break;
        }

        [UnityTest]
        public IEnumerator TestThreeArgs()
        {
            PreInstall();
            QuxInstaller.InstallFromResource("TestScriptableObjectInstallers/QuxInstaller", Container, "blurg", 2.0f, 1);

            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "blurg");
            yield break;
        }
    }
}

