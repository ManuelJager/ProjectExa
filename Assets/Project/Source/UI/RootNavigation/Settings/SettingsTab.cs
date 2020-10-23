using System;
using Exa.Data;
using Exa.UI.Components;

namespace Exa.UI.Settings
{
    /// <summary>
    /// Base class for the manager of a settings object
    /// </summary>
    /// <typeparam name="TSettings">Type of the settings object</typeparam>
    /// <typeparam name="TValues">Type of the settings values for the settings object</typeparam>
    public abstract class SettingsTab<TSettings, TValues> : SettingsTabBase
        where TSettings : SaveableSettings<TValues>, new()
        where TValues : class, IEquatable<TValues>
    {
        /// <summary>
        /// Current stored settings object
        /// </summary>
        public TSettings settings = new TSettings();

        public override bool IsDefault => settings.Values.Equals(settings.DefaultValues);
        public override bool IsDirty => !settings.Values.Equals(GetSettingsValues());

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

        public virtual void Init() {
            settings.Load();
            settings.Apply();
            settings.Save();
            ReflectValues(settings.Values);
        }

        public override void SetDefaultValues() {
            var values = settings.DefaultValues;
            ReflectValues(values);
            Apply(values);
        }

        public override void ApplyChanges() {
            Apply(GetSettingsValues());
        }

        /// <summary>
        /// Applies the given values
        /// </summary>
        /// <param name="values"></param>
        private void Apply(TValues values) {
            settings.Values = values;
            settings.Save();
            settings.Apply();
        }
    }

    public abstract class SettingsTabBase : SettingsTab
    {
        /// <summary>
        /// Denotes wether the current settings object is equal to the default values
        /// </summary>
        public abstract bool IsDefault { get; }

        public abstract bool IsDirty { get; }

        /// <summary>
        /// Applies the default values
        /// </summary>
        public abstract void SetDefaultValues();

        /// <summary>
        /// Applies the settings from the controls
        /// </summary>
        public abstract void ApplyChanges();
    }
}