using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;

namespace Exa.IO
{
    public enum SerializationMode
    {
        compact,
        readable
    }

    public static partial class IOUtils
    {
        public static JsonSerializerSettings compactJsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public static JsonSerializerSettings readableJsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public static bool TryJsonDeserializeFromPath<T>(string filePath, out T result, SerializationMode serializationMode = SerializationMode.compact)
            where T : class
        {
            if (!File.Exists(filePath))
            {
                result = null;
                return false;
            }

            try
            {
                var text = File.ReadAllText(filePath);
                result = JsonConvert.DeserializeObject<T>(text, GetSettings(serializationMode));
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static T JsonDeserializeWithSettings<T>(
            string input,
            SerializationMode serializationMode = SerializationMode.compact)
        {
            return JsonConvert.DeserializeObject<T>(input, GetSettings(serializationMode));
        }

        public static T JsonDeserializeFromPath<T>(
            string filePath,
            SerializationMode serializationMode = SerializationMode.compact)
        {
            var text = File.ReadAllText(filePath);
            return JsonDeserializeWithSettings<T>(text, serializationMode);
        }

        public static string JsonSerializeWithSettings(
            object value,
            SerializationMode serializationMode = SerializationMode.compact)
        {
            return JsonConvert.SerializeObject(value, GetSettings(serializationMode));
        }

        public static void JsonSerializeToPath(
            object value,
            string filePath,
            SerializationMode serializationMode = SerializationMode.compact)
        {
            var text = JsonSerializeWithSettings(value, serializationMode);
            File.WriteAllText(filePath, text);
        }

        private static JsonSerializerSettings GetSettings(SerializationMode serializationMode)
        {
            switch (serializationMode)
            {
                case SerializationMode.compact:
                    return compactJsonSettings;

                case SerializationMode.readable:
                    return readableJsonSettings;

                default:
                    return null;
            }
        }
    }
}