using System;

namespace UniDi
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NoReflectionBakingAttribute : Attribute
    {
    }
}
