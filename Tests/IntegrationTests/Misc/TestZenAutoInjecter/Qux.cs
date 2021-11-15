using UnityEngine;

namespace UniDi.Tests.AutoInjecter
{
    public class Qux : MonoBehaviour
    {
        [Inject]
        public DiContainer Container;
    }
}

