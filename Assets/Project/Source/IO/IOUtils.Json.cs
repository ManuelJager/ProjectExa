using System;
using System.IO;
using Newtonsoft.Json;

namespace Exa.IO {
    public enum SerializationMode {
        Compact,
        Readable,
        Settings
    }

    public static partial class IOUtils {
        private static readonly JsonSerializerSettings CompactJsonSettings = new JsonSerializerSettings {
            Formatting = Formatting.None,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings ReadableJsonSettings = new JsonSerializerSettings {
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings SettingsJsonSettings = new JsonSerializerSettings {
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.Populate
        };

        public static bool TryJsonDeserializeFromPath<T>(
            string filePath,
            out T result,
            SerializationMode serializationMode = SerializationMode.Compact
        )
            where T : class {
            if (!File.Exists(filePath)) {
                result = null;

                return false;
            }

            try {
                var text = File.ReadAllText(filePath);
                result = JsonConvert.DeserializeObject<T>(text, GetSettings(serializationMode));

                return true;
            } catch {
                result = null;

                return false;
            }
        }

        public static T FromJsonSafe<T>(
            string input,
            SerializationMode serializationMode = SerializationMode.Compact
        ) {
            try {
                return FromJson<T>(input, serializationMode);
            } catch {
                return default;
            }
        }

        public static T FromJson<T>(
            string input,
            SerializationMode serializationMode = SerializationMode.Compact
        ) {
            return JsonConvert.DeserializeObject<T>(input, GetSettings(serializationMode));
        }

        public static T FromJsonPath<T>(
            string filePath,
            SerializationMode serializationMode = SerializationMode.Compact
        ) {
            var text = File.ReadAllText(filePath);

            return FromJson<T>(text, serializationMode);
        }

        public static string ToJson(
            object value,
            SerializationMode serializationMode = SerializationMode.Compact
        ) {
            return JsonConvert.SerializeObject(value, GetSettings(serializationMode));
        }

        public static void ToJsonPath(
            object value,
            string filePath,
            SerializationMode serializationMode = SerializationMode.Compact
        ) {
            var text = ToJson(value, serializationMode);
            File.WriteAllText(filePath, text);
        }

        private static JsonSerializerSettings GetSettings(SerializationMode serializationMode) {
            return serializationMode switch {
                SerializationMode.Compact => CompactJsonSettings,
                SerializationMode.Readable => ReadableJsonSettings,
                SerializationMode.Settings => SettingsJsonSettings,
                _ => throw new ArgumentException("Unsupported serialization mode", nameof(serializationMode))
            };
        }
    }
}