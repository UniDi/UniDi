using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniDi;
using UniDi.Internal;
using Assert= UniDi.Internal.Assert;

namespace UniDi.Tests
{
    public static class TestListComparer
    {
        public static bool ContainSameElements(IEnumerable listA, IEnumerable listB)
        {
            return ContainSameElementsInternal(listA.Cast<object>().ToList(), listB.Cast<object>().ToList());
        }

        static bool ContainSameElementsInternal(
            List<object> listA, List<object> listB)
        {
            // We don't care how they are sorted as long as they are sorted the same way so just use hashcode
            
            int Comparer(object left, object right) => (left.GetHashCode().CompareTo(right.GetHashCode()));

            listA.Sort((Comparison<object>) Comparer);
            listB.Sort((Comparison<object>) Comparer);

            return Enumerable.SequenceEqual(listA, listB);
        }

        public static string PrintList<T>(List<T> list)
        {
            return list.Select(x => x.ToString()).ToArray().Join(",");
        }
    }
}
