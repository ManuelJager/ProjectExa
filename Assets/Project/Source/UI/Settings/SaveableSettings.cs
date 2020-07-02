using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Exa.UI.Settings
{
    /// <summary>
    /// Provides base functionality for a settings object that is serialized and stored in the player prefs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SaveableSettings<T> : ISettings
    {
        /// <summary>
        /// Default setting values
        /// </summary>
        public abstract T DefaultValues { get; }

        /// <summary>
        /// Current setting values
        /// </summary>
        public T Values { get; set; }

        /// <summary>
        /// Applies the current values to the client
        /// </summary>
        public abstract void Apply();

        /// <summary>
        /// Saves the settings in the player prefs
        /// </summary>
        public void Save()
        {
            var key = GetType().Name;
            PlayerPrefs.SetString(key, JsonConvert.SerializeObject(Values));
        }

        /// <summary>
        /// Loads the settings from the player prefs
        /// </summary>
        public void Load()
        {
            var key = GetType().Name;
            if (PlayerPrefs.HasKey(key))
            {
                Values = JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
            }
            else
            {
                Values = DefaultValues;
            }
        }
    }
}