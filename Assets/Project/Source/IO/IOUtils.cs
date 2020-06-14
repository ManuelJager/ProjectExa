using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Exa.IO
{
    public static class IOUtils
    {
        public static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
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
            result = JsonConvert.DeserializeObject<T>(text, jsonSettings);
            return true;
        }

        public static T JsonDeserializeFromPath<T>(string filePath)
        {
            var text = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(text, jsonSettings);
        }

        public static bool TryBinaryDeserializeFromPath<T>(string filePath, out T result)
            where T : class
        {
            if (!File.Exists(filePath))
            {
                result = null;
                return false;
            }

            using (var stream = File.OpenRead(filePath))
            {
                var formatter = new BinaryFormatter();
                result = (T)formatter.Deserialize(stream);
                return true;
            }
        }

        public static T BinaryDeserializeFromPath<T>(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void JsonSerializeToPath(string filePath, object value)
        {
            var text = JsonConvert.SerializeObject(value, jsonSettings);
            File.WriteAllText(filePath, text);
        }

        public static void BinarySerializeToPath(string filePath, object value)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            using (var stream = File.OpenWrite(filePath))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, value);
            }
        }
    }
}