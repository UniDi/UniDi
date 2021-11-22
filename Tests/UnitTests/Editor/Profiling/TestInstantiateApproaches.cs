using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using UniDi.Internal;

#pragma warning disable 414

namespace UniDi.Tests.Injection
{
    //[TestFixture]
    // Conclusion here is that the compiled expressions are basically identical to reflection
    // baking during the instantiate (though there are wins during initialization)
    // When compiled expressions are not used such as IL2CPP however there is a noticable
    // improvement of maybe 15-20% for instantiate
    public class TestInstantiateApproaches : UniDiUnitTestFixture
    {
        //[Test]
        public void TestWithoutReflectionBaking()
        {
            Log.Trace("Average without baking: {0:0.000}", Run<FooDerivedNoBaking>());
        }

        //[Test]
        public void TestWithReflectionBaking()
        {
            Log.Trace("Average with baking: {0:0.000}", Run<FooDerivedBaked>());
        }

        double Run<T>()
        {
            Container.Bind<Test0>().FromInstance(new Test0());

            // Do not include initial reflection costs
            Container.Instantiate<T>();
            Container.Instantiate<T>();

            var measurements = new List<double>();

            for (int k = 0; k < 10; k++)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                for (int i = 0; i < 10000; i++)
                {
                    Container.Instantiate<T>();
                }
                stopwatch.Stop();
                measurements.Add(stopwatch.Elapsed.TotalSeconds);
            }

            return measurements.Average();
        }

        [NoReflectionBaking]
        class Test0
        {
        }

        [NoReflectionBaking]
        abstract class FooBaseBaked
        {
            [Inject]
            public Test0 BaseFieldPublic = null;

            [Inject]
            Test0 BaseFieldPrivate = null;

            [Inject]
            protected Test0 BaseFieldProtected = null;

            [Inject]
            public Test0 BasePropertyPublic
            {
                get; set;
            }

            [Inject]
            Test0 BasePropertyPrivate
            {
                get;
                set;
            }

            [Inject]
            protected Test0 BasePropertyProtected
            {
                get;
                set;
            }

            [Inject]
            public void PostInjectBase()
            {
                DidPostInjectBase = true;
            }

            public bool DidPostInjectBase
            {
                get; private set;
            }

            private static void __uniDiFieldSetter0(object P_0, object P_1)
            {
                ((FooBaseBaked)P_0).BaseFieldPublic = (Test0)P_1;
            }

            private static void __uniDiFieldSetter1(object P_0, object P_1)
            {
                ((FooBaseBaked)P_0).BaseFieldPrivate = (Test0)P_1;
            }

            private static void __uniDiFieldSetter2(object P_0, object P_1)
            {
                ((FooBaseBaked)P_0).BaseFieldProtected = (Test0)P_1;
            }

            private static void __uniDiPropertySetter0(object P_0, object P_1)
            {
                ((FooBaseBaked)P_0).BasePropertyPublic = (Test0)P_1;
            }

            private static void __uniDiPropertySetter1(object P_0, object P_1)
            {
                ((FooBaseBaked)P_0).BasePropertyPrivate = (Test0)P_1;
            }

            private static void __uniDiPropertySetter2(object P_0, object P_1)
            {
                ((FooBaseBaked)P_0).BasePropertyProtected = (Test0)P_1;
            }

            private static void __uniDiInjectMethod0(object P_0, object[] P_1)
            {
                ((FooBaseBaked)P_0).PostInjectBase();
            }

            [Preserve]
            private static InjectTypeInfo CreateInjectTypeInfo()
            {
                return new InjectTypeInfo(typeof(FooBaseBaked), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
                {
                    new InjectTypeInfo.InjectMethodInfo(__uniDiInjectMethod0, new InjectableInfo[0], "PostInjectBase")
                }, new InjectTypeInfo.InjectMemberInfo[6]
                {
                    new InjectTypeInfo.InjectMemberInfo(__uniDiFieldSetter0, new InjectableInfo(false, null, "BaseFieldPublic", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiFieldSetter1, new InjectableInfo(false, null, "BaseFieldPrivate", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiFieldSetter2, new InjectableInfo(false, null, "BaseFieldProtected", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiPropertySetter0, new InjectableInfo(false, null, "BasePropertyPublic", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiPropertySetter1, new InjectableInfo(false, null, "BasePropertyPrivate", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiPropertySetter2, new InjectableInfo(false, null, "BasePropertyProtected", typeof(Test0), null, 0))
                });
            }
        }

        [NoReflectionBaking]
        class FooDerivedBaked : FooBaseBaked
        {
            public Test0 ConstructorParam = null;

            // Instance
            public FooDerivedBaked(Test0 param)
            {
                ConstructorParam = param;
            }

            [Inject]
            public void PostInject()
            {
            }

            [Inject]
            public Test0 DerivedFieldPublic = null;

            [Inject]
            Test0 DerivedFieldPrivate = null;

            [Inject]
            protected Test0 DerivedFieldProtected = null;

            [Inject]
            public Test0 DerivedPropertyPublic
            {
                get; set;
            }

            [Inject]
            Test0 DerivedPropertyPrivate
            {
                get;
                set;
            }

            [Inject]
            protected Test0 DerivedPropertyProtected
            {
                get;
                set;
            }

            private static object __uniDiCreate(object[] P_0)
            {
                return new FooDerivedBaked((Test0)P_0[0]);
            }

            private static void __uniDiFieldSetter0(object P_0, object P_1)
            {
                ((FooDerivedBaked)P_0).DerivedFieldPublic = (Test0)P_1;
            }

            private static void __uniDiFieldSetter1(object P_0, object P_1)
            {
                ((FooDerivedBaked)P_0).DerivedFieldPrivate = (Test0)P_1;
            }

            private static void __uniDiFieldSetter2(object P_0, object P_1)
            {
                ((FooDerivedBaked)P_0).DerivedFieldProtected = (Test0)P_1;
            }

            private static void __uniDiPropertySetter0(object P_0, object P_1)
            {
                ((FooDerivedBaked)P_0).DerivedPropertyPublic = (Test0)P_1;
            }

            private static void __uniDiPropertySetter1(object P_0, object P_1)
            {
                ((FooDerivedBaked)P_0).DerivedPropertyPrivate = (Test0)P_1;
            }

            private static void __uniDiPropertySetter2(object P_0, object P_1)
            {
                ((FooDerivedBaked)P_0).DerivedPropertyProtected = (Test0)P_1;
            }

            private static void __uniDiInjectMethod0(object P_0, object[] P_1)
            {
                ((FooDerivedBaked)P_0).PostInject();
            }

            [Preserve]
            private static InjectTypeInfo CreateInjectTypeInfo()
            {
                return new InjectTypeInfo(typeof(FooDerivedBaked), new InjectTypeInfo.InjectConstructorInfo(__uniDiCreate, new InjectableInfo[1]
                {
                    new InjectableInfo(false, null, "param", typeof(Test0), null, 0)
                }), new InjectTypeInfo.InjectMethodInfo[1]
                {
                    new InjectTypeInfo.InjectMethodInfo(__uniDiInjectMethod0, new InjectableInfo[0], "PostInject")
                }, new InjectTypeInfo.InjectMemberInfo[6]
                {
                    new InjectTypeInfo.InjectMemberInfo(__uniDiFieldSetter0, new InjectableInfo(false, null, "DerivedFieldPublic", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiFieldSetter1, new InjectableInfo(false, null, "DerivedFieldPrivate", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiFieldSetter2, new InjectableInfo(false, null, "DerivedFieldProtected", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiPropertySetter0, new InjectableInfo(false, null, "DerivedPropertyPublic", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiPropertySetter1, new InjectableInfo(false, null, "DerivedPropertyPrivate", typeof(Test0), null, 0)),
                    new InjectTypeInfo.InjectMemberInfo(__uniDiPropertySetter2, new InjectableInfo(false, null, "DerivedPropertyProtected", typeof(Test0), null, 0))
                });
            }
        }

        [NoReflectionBaking]
        abstract class FooBaseNoBaking
        {
            [Inject]
            public Test0 BaseFieldPublic = null;

            [Inject]
            Test0 BaseFieldPrivate = null;

            [Inject]
            protected Test0 BaseFieldProtected = null;

            [Inject]
            public Test0 BasePropertyPublic
            {
                get; set;
            }

            [Inject]
            Test0 BasePropertyPrivate
            {
                get;
                set;
            }

            [Inject]
            protected Test0 BasePropertyProtected
            {
                get;
                set;
            }

            [Inject]
            public void PostInjectBase()
            {
                DidPostInjectBase = true;
            }

            public bool DidPostInjectBase
            {
                get; private set;
            }
        }

        [NoReflectionBaking]
        class FooDerivedNoBaking : FooBaseNoBaking
        {
            public Test0 ConstructorParam = null;

            // Instance
            public FooDerivedNoBaking(Test0 param)
            {
                ConstructorParam = param;
            }

            [Inject]
            public void PostInject()
            {
            }

            [Inject]
            public Test0 DerivedFieldPublic = null;

            [Inject]
            Test0 DerivedFieldPrivate = null;

            [Inject]
            protected Test0 DerivedFieldProtected = null;

            [Inject]
            public Test0 DerivedPropertyPublic
            {
                get; set;
            }

            [Inject]
            Test0 DerivedPropertyPrivate
            {
                get;
                set;
            }

            [Inject]
            protected Test0 DerivedPropertyProtected
            {
                get;
                set;
            }
        }
    }
}
