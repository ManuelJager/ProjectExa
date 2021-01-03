using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Utils
{
    public static class EnumerableUtils
    {
        public static IEnumerable<(T1, T2)> AsTupleEnumerable<T1, T2>(this IEnumerable<T1> first,
            IEnumerable<T2> second) {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext()) {
                yield return (firstEnumerator.Current, secondEnumerator.Current);
            }
        }

        public static IEnumerable<(T1, T2, T3)> AsTupleEnumerable<T1, T2, T3>(this IEnumerable<T1> first,
            IEnumerable<T2> second, IEnumerable<T3> third) {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            var thirdEnumerator = third.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext() && thirdEnumerator.MoveNext()) {
                yield return (firstEnumerator.Current, secondEnumerator.Current, thirdEnumerator.Current);
            }
        }

        public static IEnumerable<(T1, T2, T3, T4)> AsTupleEnumerable<T1, T2, T3, T4>(this IEnumerable<T1> first,
            IEnumerable<T2> second, IEnumerable<T3> third, IEnumerable<T4> fourth) {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            var thirdEnumerator = third.GetEnumerator();
            var fourthEnumerator = fourth.GetEnumerator();

            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext() && thirdEnumerator.MoveNext() &&
                   fourthEnumerator.MoveNext()) {
                yield return (firstEnumerator.Current, secondEnumerator.Current, thirdEnumerator.Current, 
                    fourthEnumerator.Current);
            }
        }

        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (var element in enumerable) {
                action(element);
            }
        }

        public static TTarget FindFirst<TTarget>(this IEnumerable enumerable) {
            foreach (var elem in enumerable) {
                if (elem is TTarget target) {
                    return target;
                }
            }

            return default;
        }
    }
}