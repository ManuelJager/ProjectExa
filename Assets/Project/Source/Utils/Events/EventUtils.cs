using UnityEngine.Events;

namespace Exa.Utils
{
    public static class EventUtils
    {
        /// <summary>
        /// Add a listener to the event, and returns a handle to later remove the listener
        /// </summary>
        /// <param name="unityEvent">Event</param>
        /// <param name="action">Event listener</param>
        /// <returns>A handle to the event listener</returns>
        public static EventRef AddListenerWithRef(
            this UnityEvent unityEvent,
            UnityAction action)
        {
            unityEvent.AddListener(action);
            return new EventRef(() => unityEvent.RemoveListener(action));
        }

        /// <summary>
        /// Adds a listener to the event and returns a handle to later remove the listener
        /// </summary>
        /// <param name="unityEvent"></param>
        /// <param name="action"></param>
        /// <param name="eventRef"></param>
        public static void AddListenerOnce(
            this UnityEvent unityEvent,
            UnityAction action,
            ref EventRef eventRef)
        {
            eventRef?.RemoveListenerFromTarget();
            eventRef = AddListenerWithRef(unityEvent, action);
        }

        /// <inheritdoc cref="AddListenerWithRef(UnityEvent, UnityAction)"/>
        public static EventRef AddListenerWithRef<T0>(
            this UnityEvent<T0> unityEvent,
            UnityAction<T0> action)
        {
            unityEvent.AddListener(action);
            return new EventRef(() => unityEvent.RemoveListener(action));
        }

        /// <inheritdoc cref="AddListenerOnce(UnityEvent, UnityAction, ref EventRef)"/>
        public static void AddListenerOnce<T0>(
            this UnityEvent<T0> unityEvent,
            UnityAction<T0> action,
            ref EventRef eventRef)
        {
            eventRef?.RemoveListenerFromTarget();
            eventRef = AddListenerWithRef(unityEvent, action);
        }

        /// <inheritdoc cref="AddListenerWithRef(UnityEvent, UnityAction)"/>
        public static EventRef AddListenerWithRef<T0, T1>(
            this UnityEvent<T0, T1> unityEvent,
            UnityAction<T0, T1> action)
        {
            unityEvent.AddListener(action);
            return new EventRef(() => unityEvent.RemoveListener(action));
        }

        /// <inheritdoc cref="AddListenerOnce(UnityEvent, UnityAction, ref EventRef)"/>
        public static void AddListenerOnce<T0, T1>(
            this UnityEvent<T0, T1> unityEvent,
            UnityAction<T0, T1> action,
            ref EventRef eventRef)
        {
            eventRef?.RemoveListenerFromTarget();
            eventRef = AddListenerWithRef(unityEvent, action);
        }

        /// <inheritdoc cref="AddListenerWithRef(UnityEvent, UnityAction)"/>
        public static EventRef AddListenerWithRef<T0, T1, T2>(
            this UnityEvent<T0, T1, T2> unityEvent,
            UnityAction<T0, T1, T2> action)
        {
            unityEvent.AddListener(action);
            return new EventRef(() => unityEvent.RemoveListener(action));
        }

        /// <inheritdoc cref="AddListenerOnce(UnityEvent, UnityAction, ref EventRef)"/>
        public static void AddListenerOnce<T0, T1, T2>(
            this UnityEvent<T0, T1, T2> unityEvent,
            UnityAction<T0, T1, T2> action,
            ref EventRef eventRef)
        {
            eventRef?.RemoveListenerFromTarget();
            eventRef = AddListenerWithRef(unityEvent, action);
        }

        /// <inheritdoc cref="AddListenerWithRef(UnityEvent, UnityAction)"/>
        public static EventRef AddListenerWithRef<T0, T1, T2, T3>(
            this UnityEvent<T0, T1, T2, T3> unityEvent,
            UnityAction<T0, T1, T2, T3> action)
        {
            unityEvent.AddListener(action);
            return new EventRef(() => unityEvent.RemoveListener(action));
        }

        /// <inheritdoc cref="AddListenerOnce(UnityEvent, UnityAction, ref EventRef)"/>
        public static void AddListenerOnce<T0, T1, T2, T3>(
            this UnityEvent<T0, T1, T2, T3> unityEvent,
            UnityAction<T0, T1, T2, T3> action,
            ref EventRef eventRef)
        {
            eventRef?.RemoveListenerFromTarget();
            eventRef = AddListenerWithRef(unityEvent, action);
        }
    }
}