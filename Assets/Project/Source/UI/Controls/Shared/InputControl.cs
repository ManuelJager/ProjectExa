using System;
using UnityEngine;

namespace Exa.UI.Components
{
    public abstract class InputControl<T> : InputControl
        where T : IEquatable<T>
    {
        /// <summary>
        /// Gets the unupdated value
        /// </summary>
        public abstract T CleanValue { get; set; }

        /// <summary>
        /// Gets the current value
        /// </summary>
        public abstract T Value { get; set; }

        /// <inheritdoc cref="InputControl.IsDirty"/>
        public override bool IsDirty => !CleanValue.Equals(Value);

        /// <inheritdoc cref="InputControl.SetClean()"/>
        public override void SetClean()
        {
            CleanValue = Value;
        }

        public virtual void OnEnable()
        {
            SetClean();
        }
    }

    public abstract class InputControl : MonoBehaviour, IControl
    {
        /// <summary>
        /// Checks if the value is up-to-date
        /// </summary>
        public abstract bool IsDirty { get; }

        /// <summary>
        /// Marks the control as being updated
        /// </summary>
        public abstract void SetClean();
    }
}