using UnityEngine;

namespace UniDi.Tests.Bindings.FromSubContainerPrefab
{
    public class CircFoo : MonoBehaviour
    {
        [Inject]
        public CircBar Bar;
    }
}
