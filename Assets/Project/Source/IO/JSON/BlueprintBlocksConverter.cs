using Exa.Grids.Blueprints;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.IO.Json
{
    public class BlueprintBlocksConverter : JsonConverter<BlueprintBlocks>
    {
        public static readonly string[] SEPARATORS = new[] { "(", ", ", ")" };

        public override BlueprintBlocks ReadJson(JsonReader reader, Type objectType, BlueprintBlocks existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            var dict = new BlueprintBlocks();
            foreach (var pair in JObject.Load(reader))
            {
                var vector = pair.Key.Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries);

                var key = new Vector2Int
                {
                    x = Convert.ToInt32(vector.First()),
                    y = Convert.ToInt32(vector.Last())
                };

                var value = pair.Value.ToObject<BlueprintBlock>(serializer);
                dict.Add(key, value);
            }
            return dict;
        }

        public override void WriteJson(JsonWriter writer, BlueprintBlocks value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var pair in value)
            {
                writer.WritePropertyName(pair.Key.ToString());
                serializer.Serialize(writer, pair.Value);
            }
            writer.WriteEndObject();
        }
    }
}