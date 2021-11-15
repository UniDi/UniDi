using System;
using System.Collections;
using System.Collections.Generic;
using UniDi.Internal.Util;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniDi;
using UniDi.Tests.TestAnimationStateBehaviourInject;

namespace UniDi.Tests.Misc.TestMonoKernelDecoration
{
    public class TestMonoKernelDecoration : UniDiIntegrationTestFixture
    {
        
        [UnityTest]
        public IEnumerator TestDelayedMonoKernelDecorator()
        {
            PreInstall();

            Container.Rebind<InitializableManager>().To<InitializableManagerSpy>().AsCached();
            KernelDecoratorInstaller.Install(Container);
            PostInstall();
            
            yield return new WaitForSeconds(1.0f);

            InitializableManagerSpy initializableManager = SceneContext.Container.Resolve<InitializableManager>() as InitializableManagerSpy;
            var initializedBeforeDelay = initializableManager.IsInitialized;
            
            yield return new WaitForSeconds(6.0f);
            var initializedAfterDelay = initializableManager.IsInitialized;

            Assert.IsFalse(initializedBeforeDelay);
            Assert.IsTrue(initializedAfterDelay);
        }
        
        private class InitializableManagerSpy : InitializableManager
        {
            
            public InitializableManagerSpy(List<IInitializable> initializables, List<ValuePair<Type, int>> priorities) : base(initializables, priorities){}

            public bool IsInitialized => _hasInitialized;
        }
        
        
    }
}