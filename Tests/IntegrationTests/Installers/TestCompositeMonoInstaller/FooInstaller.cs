using UnityEngine;
using UniDi;

namespace UniDi.Tests.Installers.CompositeMonoInstallers
{
    public class FooInstaller : MonoInstaller<FooInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Foo>().AsSingle().NonLazy();
        }
    }
}