using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace UniDi
{
    public delegate void UniDiInjectMethod(object obj, object[] args);
    public delegate object UniDiFactoryMethod(object[] args);
    public delegate void UniDiMemberSetterMethod(object obj, object value);

    [NoReflectionBaking]
    public class InjectTypeInfo
    {
        public readonly Type Type;
        public readonly InjectMethodInfo[] InjectMethods;
        public readonly InjectMemberInfo[] InjectMembers;
        public readonly InjectConstructorInfo InjectConstructor;

        public InjectTypeInfo(
            Type type,
            InjectConstructorInfo injectConstructor,
            InjectMethodInfo[] injectMethods,
            InjectMemberInfo[] injectMembers)
        {
            Type = type;
            InjectMethods = injectMethods;
            InjectMembers = injectMembers;
            InjectConstructor = injectConstructor;
        }

        // Filled in later
        public InjectTypeInfo BaseTypeInfo
        {
            get; set;
        }

        public IEnumerable<InjectableInfo> AllInjectables
        {
            get
            {
                return InjectConstructor.Parameters
                    .Concat(InjectMembers.Select(x => x.Info))
                    .Concat(InjectMethods.SelectMany(x => x.Parameters));
            }
        }

        [NoReflectionBaking]
        public class InjectMemberInfo
        {
            public readonly UniDiMemberSetterMethod Setter;
            public readonly InjectableInfo Info;

            public InjectMemberInfo(
                UniDiMemberSetterMethod setter,
                InjectableInfo info)
            {
                Setter = setter;
                Info = info;
            }
        }

        [NoReflectionBaking]
        public class InjectConstructorInfo
        {
            // Null for abstract types
            public readonly UniDiFactoryMethod Factory;

            public readonly InjectableInfo[] Parameters;

            public InjectConstructorInfo(
                UniDiFactoryMethod factory,
                InjectableInfo[] parameters)
            {
                Parameters = parameters;
                Factory = factory;
            }
        }

        [NoReflectionBaking]
        public class InjectMethodInfo
        {
            public readonly string Name;
            public readonly UniDiInjectMethod Action;
            public readonly InjectableInfo[] Parameters;

            public InjectMethodInfo(
                UniDiInjectMethod action,
                InjectableInfo[] parameters,
                string name)
            {
                Parameters = parameters;
                Action = action;
                Name = name;
            }
        }
    }
}
