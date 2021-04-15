using System.Collections.Generic;
using UniDi;

namespace UniDi
{
    public interface ICompositeInstaller<out T> : IInstaller where T : IInstaller
    {
        IReadOnlyList<T> LeafInstallers { get; }
    }
}