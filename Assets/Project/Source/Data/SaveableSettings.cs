using Exa.IO;
using System.IO;

namespace Exa.Data
{
    /// <summary>
    /// Provides base functionality for a settings object that is serialized and stored in the data directory
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

        public virtual void Save()
        {
            var path = IoUtils.CombineWithDirectory("settings", $"{Key}.json");
            IoUtils.JsonSerializeToPath(Values, path, SerializationMode.Readable);
        }

        public virtual void Load()
        {
            var path = IoUtils.CombineWithDirectory("settings", $"{Key}.json");
            Values = File.Exists(path)
                ? IoUtils.JsonDeserializeFromPath<T>(path)
                : DefaultValues;
        }
    }
}