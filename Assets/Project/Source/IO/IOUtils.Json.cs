using Newtonsoft.Json;
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
        public static JsonSerializerSettings systemJsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public static JsonSerializerSettings userJsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public static bool TryJsonDeserializeFromPath<T>(string filePath, out T result)
            where T : class
        {
            if (!File.Exists(filePath))
            {
                result = null;
                return false;
            }

            var text = File.ReadAllText(filePath);
            result = JsonConvert.DeserializeObject<T>(text, systemJsonSettings);
            return true;
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
                    return systemJsonSettings;

                case SerializationMode.readable:
                    return userJsonSettings;

                default:
                    return null;
            }
        }
    }
}