using JetBrains.Annotations;

namespace UniDi
{
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    public abstract class InjectAttributeBase : UniDi.Internal.PreserveAttribute
    {
        public bool Optional
        {
            get;
            set;
        }

        public object Id
        {
            get;
            set;
        }

        public InjectSources Source
        {
            get;
            set;
        }
    }
}
