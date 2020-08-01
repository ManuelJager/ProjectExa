using Exa.Data;
using Exa.UI.Components;
using System.Collections.Generic;
using System.Linq;

namespace Exa.UI.Settings
{
    /// <summary>
    /// Base class for the manager of a settings object
    /// </summary>
    /// <typeparam name="TSettings">Type of the settings object</typeparam>
    /// <typeparam name="TValues">Type of the settings values for the settings object</typeparam>
    public abstract class SettingsTab<TSettings, TValues> : SettingsTabBase
        where TSettings : SaveableSettings<TValues>, new()
    {
        /// <summary>
        /// Current stored settings object
        /// </summary>
        public TSettings current;

        public override bool IsDefault => current.Values.Equals(current.DefaultValues);

        /// <summary>
        /// Reflects the values of the settings
        /// </summary>
        /// <param name="values"></param>
        public abstract void ReflectValues(TValues values);

        /// <summary>
        /// Gets a settings object
        /// </summary>
        /// <returns></returns>
        public abstract TValues GetSettingsValues();

        public override void SetDefaultValues()
        {
            Apply(current.DefaultValues);
        }

        public override void ApplyChanges()
        {
            Apply(GetSettingsValues());
        }

        /// <summary>
        /// Applies the given values
        /// </summary>
        /// <param name="values"></param>
        private void Apply(TValues values)
        {
            current.Values = values;
            current.Save();
            current.Apply();
            ReflectValues(values);
            SetClean();
        }
    }

    public abstract class SettingsTabBase : Tab, IControl
    {
        /// <summary>
        /// Denotes wether any of the setting controls are not up-to-date
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return GetControls().Any(control => control.IsDirty);
            }
        }

        /// <summary>
        /// Denotes wether the current settings object is equal to the default values
        /// </summary>
        public abstract bool IsDefault { get; }

        /// <summary>
        /// All controls under the panel
        /// </summary>
        protected abstract IEnumerable<InputControl> GetControls();

        /// <summary>
        /// Applies the default values
        /// </summary>
        public abstract void SetDefaultValues();

        /// <summary>
        /// Applies the settings from the controls
        /// </summary>
        public abstract void ApplyChanges();

        /// <summary>
        /// Marks all the controls as being up-to-date
        /// </summary>
        public void SetClean()
        {
            foreach (var control in GetControls())
            {
                control.SetClean();
            }
        }
    }
}