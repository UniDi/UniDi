using UnityEngine;
using UniDi;

namespace UniDi.Tests.Installers.CompositeScriptableObjectInstallers
{
    public class FooInjectee
    {
        public FooInjectee(Foo foo)
        {
            Foo = foo;
        }

        public Foo Foo { get; }
    }
}