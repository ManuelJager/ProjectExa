using Exa.Grids.Blueprints;
using Exa.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.IO.Json
{
    public class BlueprintBlocksConverter : JsonConverter<BlueprintBlocks>
    {
        public static readonly string[] Separators = new[] { "(", ",", ")" };

        public override BlueprintBlocks ReadJson(JsonReader reader, Type objectType, BlueprintBlocks existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            var blocks = new BlueprintBlocks();
            foreach (var pair in JObject.Load(reader))
            {
                var vector = pair.Key.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                var key = new Vector2Int
                {
                    x = Convert.ToInt32(vector.First()),
                    y = Convert.ToInt32(vector.Last())
                };

                var value = pair.Value.ToObject<BlueprintBlock>(serializer);

                blocks.Add(new AnchoredBlueprintBlock(key, value));
            }
            return blocks;
        }

        public override void WriteJson(JsonWriter writer, BlueprintBlocks value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var pair in value)
            {
                writer.WritePropertyName(pair.GridAnchor.ToShortString());
                serializer.Serialize(writer, pair.BlueprintBlock);
            }
            writer.WriteEndObject();
        }
    }
}