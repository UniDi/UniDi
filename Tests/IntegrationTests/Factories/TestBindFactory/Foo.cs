using UnityEngine;

namespace UniDi.Tests.Factories.BindFactory
{
    public interface IFoo
    {
    }

    public class IFooFactory : PlaceholderFactory<IFoo>
    {
    }

    public class Foo : MonoBehaviour, IFoo
    {
        public class Factory : PlaceholderFactory<Foo>
        {
        }
    }
}
