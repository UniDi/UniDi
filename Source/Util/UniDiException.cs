using System;
using System.Diagnostics;

namespace UniDi
{
    [DebuggerStepThrough]
    [NoReflectionBaking]
    public class UniDiException : Exception
    {
        public UniDiException(string message)
            : base(message)
        {
        }

        public UniDiException(
            string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
