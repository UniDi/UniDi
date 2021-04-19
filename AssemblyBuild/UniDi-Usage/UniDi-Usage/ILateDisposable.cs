using System;

namespace UniDi
{
    public interface ILateDisposable
    {
        void LateDispose();
    }
}
