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
        private static string baseDirectory;

        /// <summary>
        /// Default setting values
        /// </summary>
        public abstract T DefaultValues { get; }

        /// <summary>
        /// Current setting values
        /// </summary>
        public T Values { get; set; }

        protected abstract string Key { get; }

        static SaveableSettings()
        {
            baseDirectory = IOUtils.CombinePathWithDataPath("settings");
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }
        }

        /// <summary>
        /// Applies the current values to the client
        /// </summary>
        public abstract void Apply();

        /// <summary>
        /// Saves the settings in the player prefs
        /// </summary>
        public virtual void Save()
        {
            var path = IOUtils.CombinePath(baseDirectory, $"{Key}.json");
            IOUtils.JsonSerializeToPath(Values, path, SerializationMode.readable);
        }

        /// <summary>
        /// Loads the settings from the player prefs
        /// </summary>
        public virtual void Load()
        {
            var path = IOUtils.CombinePath(baseDirectory, $"{Key}.json");
            Values = File.Exists(path) 
                ? IOUtils.JsonDeserializeFromPath<T>(path)
                : DefaultValues;
        }
    }
}