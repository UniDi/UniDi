
using System.Collections;
using UniDi.Internal;
using UnityEngine.TestTools;

namespace UniDi.Tests.TestAnimationStateBehaviourInject
{
    public class TestAnimationStateBehaviourInject : UniDiIntegrationTestFixture
    {
        const string ResourcePrefix = "TestAnimationStateBehaviourInject/";

        [UnityTest]
        public IEnumerator Test1()
        {
            PreInstall();
            var prefab = FixtureUtil.GetPrefab(ResourcePrefix + "Foo");

            StateBehaviour1.OnStateEnterCalls = 0;

            Container.InstantiatePrefab(prefab);
            Container.BindInterfacesAndSelfTo<Foo>().AsSingle();
            PostInstall();

            yield return null;

            Assert.IsEqual(StateBehaviour1.OnStateEnterCalls, 1);
        }

        public class Foo : IInitializable
        {
            public bool HasInitialized
            {
                get; private set;
            }

            public void Initialize()
            {
                HasInitialized = true;
            }
        }
    }
}

