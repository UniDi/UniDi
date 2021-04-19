using System;

namespace UniDi
{
    [AttributeUsage(AttributeTargets.Constructor
        | AttributeTargets.Method | AttributeTargets.Parameter
        | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class InjectAttribute : InjectAttributeBase
    {
    }
}

