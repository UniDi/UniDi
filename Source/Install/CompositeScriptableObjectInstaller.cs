using System.Collections.Generic;
using UnityEngine;
using UniDi.Internal;

namespace UniDi
{
    // Use `Create -> UniDi -> Composite Scriptable Object Installer`
    public class CompositeScriptableObjectInstaller : ScriptableObjectInstaller<CompositeScriptableObjectInstaller>, ICompositeInstaller<ScriptableObjectInstallerBase>
    {
        [SerializeField]
        List<ScriptableObjectInstallerBase> _leafInstallers = new List<ScriptableObjectInstallerBase>();
        public IReadOnlyList<ScriptableObjectInstallerBase> LeafInstallers => _leafInstallers;

        public override void InstallBindings()
        {
            Assert.That(this.ValidateLeafInstallers(), "Found some circular references in {0}".Fmt(name));

            foreach (var installer in _leafInstallers)
            {
                Container.Inject(installer);

#if UNIDI_INTERNAL_PROFILING
                using (ProfileTimers.CreateTimedBlock("User Code"))
#endif
                {
                    installer.InstallBindings();
                }
            }
        }
    }
}