using System;
using System.Collections.Generic;
using UniDi.Internal;

namespace UniDi
{
    [NoReflectionBaking]
    public class SubContainerCreatorCached : ISubContainerCreator
    {
        readonly ISubContainerCreator _subCreator;

#if UNIDI_MULTITHREADING
        readonly object _locker = new object();
#else
        bool _isLookingUp;
#endif
        DiContainer _subContainer;

        public SubContainerCreatorCached(ISubContainerCreator subCreator)
        {
            _subCreator = subCreator;
        }

        public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext context, out Action injectAction)
        {
            // We can't really support arguments if we are using the cached value since
            // the arguments might change when called after the first time
            Assert.IsEmpty(args);

#if UNIDI_MULTITHREADING
            lock (_locker)
#endif
            {
                if (_subContainer == null)
                {
#if !UNIDI_MULTITHREADING
                    Assert.That(!_isLookingUp,
                        "Found unresolvable circular dependency when looking up sub container!  Object graph:\n {0}", context.GetObjectGraphString());
                    _isLookingUp = true;
#endif

                    _subContainer = _subCreator.CreateSubContainer(
                            new List<TypeValuePair>(), context, out injectAction);

#if !UNIDI_MULTITHREADING
                    _isLookingUp = false;
#endif

                    Assert.IsNotNull(_subContainer);
                }
                else 
                {
                    injectAction = null;
                }

                return _subContainer;
            }
        }
    }
}
