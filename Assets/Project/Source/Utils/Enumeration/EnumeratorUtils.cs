using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Utils
{
    public static class EnumeratorUtils
    {
        public static Coroutine DelayOneFrame(this MonoBehaviour monoBehaviour, Action callback)
        {
            return monoBehaviour.StartCoroutine(DelayOneFrame(callback));
        }

        public static IEnumerable Enumerate(IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerator ScheduleWithFramerate(IEnumerator enumerator, float framerate)
        {
            var maxTimeDelta = 1.0f / framerate;
            var timeStamp = Time.realtimeSinceStartup;

            while(enumerator.MoveNext())
            {
                if (Time.realtimeSinceStartup > timeStamp + maxTimeDelta)
                {
                    yield return new WaitForEndOfFrame();
                    timeStamp = Time.realtimeSinceStartup;
                }
            }
        }

        public static IEnumerator ReportForeachOperation<T>(IEnumerable<T> enumerable, Func<T, IEnumerator> func, IProgress<float> progress)
        {
            var index = 0;
            var list = enumerable.ToList();
            progress.Report(0);

            foreach (var item in list)
            {
                var enumerator = func(item);
                while (enumerator.MoveNext()) yield return enumerator.Current;

                index++;
                progress.Report((float)index / list.Count);
            }
        }

        public static IEnumerator ReportForeachOperation<T>(IEnumerable<T> enumerable, Action<T> action, IProgress<float> progress)
        {
            var index = 0;
            var list = enumerable.ToList();
            progress.Report(0);

            foreach (var item in list)
            {
                action(item);
                yield return null;

                index++;
                progress.Report((float)index / list.Count);
            }
        }

        public static IEnumerator DelayOneFrame(Action callback)
        {
            yield return 0;

            callback();
        }

        public static IEnumerator Delay(Action callback, float seconds)
        {
            if (seconds > 0f)
            {
                yield return new WaitForSeconds(seconds);
            }

            callback();
        }
    }
}