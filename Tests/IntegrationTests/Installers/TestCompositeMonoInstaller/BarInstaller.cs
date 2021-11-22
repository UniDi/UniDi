using UnityEngine;
using UniDi;

namespace UniDi.Tests.Installers.CompositeMonoInstallers
{
    public class BarInstaller : MonoInstaller<BarInstaller>
    {
        [SerializeField] string _value;

        public override void InstallBindings()
        {
            Container.BindInstance(_value);
        }
    }
}
