using Exa.UI.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        protected TSettings current;

        public void OnEnable()
        {
            current = new TSettings();
            current.Load();
            current.Apply();
            ReflectValues(current.Values);
        }

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
            MarkClean();
        }
    }

    public abstract class SettingsTabBase : Tab, IDirtableControl
    {
        /// <summary>
        /// All controls under the panel
        /// </summary>
        [SerializeField] protected List<InputControl> controls;

        /// <summary>
        /// Gets wether any of the setting controls are not up-to-date
        /// </summary>
        public bool IsDirty => controls.Any(control => control.IsDirty);

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
        public void MarkClean()
        {
            foreach (var control in controls)
            {
                control.MarkClean();
            }
        }
    }
}