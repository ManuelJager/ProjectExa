using System;
using System.Collections.Generic;

namespace Exa.Utils
{
    public static class EnumerableUtils
    {
        public static IEnumerable<Tuple<T1, T2>> AsTupleEnumerable<T1, T2>(IEnumerable<T1> first,
            IEnumerable<T2> second) {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext()) {
                yield return new Tuple<T1, T2>(firstEnumerator.Current, secondEnumerator.Current);
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3>> AsTupleEnumerable<T1, T2, T3>(IEnumerable<T1> first,
            IEnumerable<T2> second, IEnumerable<T3> third) {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            var thirdEnumerator = third.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext() && thirdEnumerator.MoveNext()) {
                yield return new Tuple<T1, T2, T3>(firstEnumerator.Current, secondEnumerator.Current,
                    thirdEnumerator.Current);
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4>> AsTupleEnumerable<T1, T2, T3, T4>(IEnumerable<T1> first,
            IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> fourth) {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            var thirdEnumerator = third.GetEnumerator();
            var fourthEnumerator = fourth.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext() && thirdEnumerator.MoveNext() &&
                   fourthEnumerator.MoveNext()) {
                yield return new Tuple<T1, T2, T3, T4>(firstEnumerator.Current, secondEnumerator.Current,
                    thirdEnumerator.Current, fourthEnumerator.Current);
            }
        }
    }
}