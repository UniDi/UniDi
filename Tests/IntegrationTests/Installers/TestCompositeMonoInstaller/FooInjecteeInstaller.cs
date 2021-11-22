using UnityEngine;
using UniDi;

namespace UniDi.Tests.Installers.CompositeMonoInstallers
{
    public class FooInjecteeInstaller : MonoInstaller<FooInjecteeInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<FooInjectee>()
                .AsSingle()
                .NonLazy();
        }
    }
}