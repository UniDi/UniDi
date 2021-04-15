using UnityEditor;

namespace UniDi
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CompositeMonoInstaller))]
    [NoReflectionBaking]
    public class CompositeMonoInstallerEditor : BaseCompositetInstallerEditor<CompositeMonoInstaller, MonoInstallerBase>
    {
    }
}