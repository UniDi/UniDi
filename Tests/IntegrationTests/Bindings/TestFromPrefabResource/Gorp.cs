using UniDi.Internal;
using UnityEngine;

#pragma warning disable 649

namespace UniDi.Tests.Bindings.FromPrefabResource
{
    public class Gorp : MonoBehaviour
    {
        [Inject]
        string _arg;

        [Inject]
        public void Initialize()
        {
            Log.Trace("Received arg '{0}' in Gorp", _arg);
        }
    }
}
