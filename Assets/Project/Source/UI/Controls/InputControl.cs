using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public abstract class InputControl<T> : InputControl
    {
        /// <summary>
        /// Gets the unupdated value
        /// </summary>
        public abstract T CleanValue { get; set; }

        /// <summary>
        /// Gets the current value
        /// </summary>
        public abstract T Value { get; set; }

        public abstract UnityEvent<T> OnValueChange { get; }

        /// <inheritdoc cref="InputControl.IsDirty"/>
        public override bool IsDirty => !CleanValue.Equals(Value);

        /// <inheritdoc cref="InputControl.SetClean()"/>
        public override void SetClean()
        {
            CleanValue = Value;
        }

        protected virtual void OnEnable()
        {
            SetClean();
        }
    }

    public abstract class InputControl : MonoBehaviour, IControl
    {
        [SerializeField] private Text labelText;

        /// <summary>
        /// Checks if the value is up-to-date
        /// </summary>
        public abstract bool IsDirty { get; }

        /// <summary>
        /// Marks the control as being updated
        /// </summary>
        public abstract void SetClean();

        public void SetLabelText(string label)
        {
            labelText.text = label;
        }
    }
}