using System.Collections;
using UniDi.Internal;
using UnityEngine;
using UnityEngine.TestTools;
using UniDi;
using UniDi.Tests;
using UniDi.Tests.Installers.CompositeMonoInstallers;

namespace UniDi.Tests.Installers
{
    public class TestCompositeMonoInstallers : UniDiIntegrationTestFixture
    {
        [UnityTest]
        public IEnumerator TestZeroParameters()
        {
            PreInstall();
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/FooInstaller/TestCompositeMonoFooInstaller", Container);
            PostInstall();

            FixtureUtil.AssertResolveCount<Foo>(Container, 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestZeroParametersDeep()
        {
            PreInstall();
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/FooInstaller/TestCompositeMonoDeepFooInstaller1", Container);
            PostInstall();

            FixtureUtil.AssertResolveCount<Foo>(Container, 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestOneParameter()
        {
            PreInstall();
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/BarInstaller/TestCompositeMonoBarInstaller", Container);
            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "composite mono installer blurg");
            yield break;
        }

        [UnityTest]
        public IEnumerator TestOneParameterDeep()
        {
            PreInstall();
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/BarInstaller/TestCompositeMonoDeepBarInstaller1", Container);
            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "composite mono installer blurg");
            yield break;
        }

        [UnityTest]
        public IEnumerator TestThreeParameters()
        {
            PreInstall();
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/QuxInstaller/TestCompositeMonoQuxInstaller", Container);
            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "composite mono installer string");
            Assert.IsEqual(Container.Resolve<float>(), 23.45f);
            Assert.IsEqual(Container.Resolve<int>(), 7890);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestThreeParametersDeep()
        {
            PreInstall();
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/QuxInstaller/TestCompositeMonoDeepQuxInstaller1", Container);
            PostInstall();

            Assert.IsEqual(Container.Resolve<string>(), "composite mono installer string");
            Assert.IsEqual(Container.Resolve<float>(), 23.45f);
            Assert.IsEqual(Container.Resolve<int>(), 7890);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestMultipleInstallers()
        {
            PreInstall();
            FooInjecteeInstaller.InstallFromResource("TestCompositeMonoInstallers/FooInjecteeInstaller/FooInjecteeInstaller", Container);
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/FooInstaller/TestCompositeMonoFooInstaller", Container);
            PostInstall();

            FixtureUtil.AssertResolveCount<Foo>(Container, 1);
            FixtureUtil.AssertResolveCount<FooInjectee>(Container, 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestMultipleInstallersDeep()
        {
            PreInstall();
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/FooInjecteeInstaller/TestCompositeMonoFooInjecteeInstaller", Container);
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/FooInstaller/TestCompositeMonoFooInstaller", Container);
            PostInstall();

            FixtureUtil.AssertResolveCount<Foo>(Container, 1);
            FixtureUtil.AssertResolveCount<FooInjectee>(Container, 1);
            yield break;
        }

        [UnityTest]
        public IEnumerator TestDuplicateInstallers()
        {
            PreInstall();
            InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/FooInstaller/TestCompositeMonoFooInstaller", Container);
            Assert.Throws<UniDiException>(() =>
            {
                InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/FooInstaller/TestCompositeMonoDeepFooInstaller1", Container);
            });
            PostInstall();

            yield break;
        }

        [UnityTest]
        public IEnumerator TestAssertWithCircularReference()
        {
            PreInstall();

            Assert.Throws<UniDiException>(() =>
            {
                InstallCompositeMonoInstallerFromResource("TestCompositeMonoInstallers/CircularReferenceCompositeInstaller/CircularReferenceCompositeMonoInstaller", Container);
            });

            PostInstall();

            yield break;
        }

        // An installation method for "CompositeMonoInstaller".
        // MonoInstaller.InstallFromResource uses "GetComponentsInChildren", so it can't be used for "CompositeMonoInstaller" if the prefab has multiple "CompositeMonoInstaller".
        public static void InstallCompositeMonoInstallerFromResource(string resourcePath, DiContainer container)
        {
            var installerPrefab = Resources.Load<CompositeMonoInstaller>(resourcePath);
            var installer = GameObject.Instantiate(installerPrefab);
            container.Inject(installer);
            installer.InstallBindings();
        }
    }
}
