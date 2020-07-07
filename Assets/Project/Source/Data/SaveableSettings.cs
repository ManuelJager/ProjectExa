using Exa.IO;
using System.IO;

namespace Exa.Data
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

        protected abstract string Key { get; }

        /// <summary>
        /// Applies the current values to the client
        /// </summary>
        public abstract void Apply();

        /// <summary>
        /// Saves the settings in the player prefs
        /// </summary>
        public virtual void Save()
        {
            var path = IOUtils.CombinePath(IOUtils.GetPath("settings"), $"{Key}.json");
            IOUtils.JsonSerializeToPath(Values, path, SerializationMode.readable);
        }

        /// <summary>
        /// Loads the settings from the player prefs
        /// </summary>
        public virtual void Load()
        {
            var path = IOUtils.CombinePath(IOUtils.GetPath("settings"), $"{Key}.json");
            Values = File.Exists(path)
                ? IOUtils.JsonDeserializeFromPath<T>(path)
                : DefaultValues;
        }
    }
}