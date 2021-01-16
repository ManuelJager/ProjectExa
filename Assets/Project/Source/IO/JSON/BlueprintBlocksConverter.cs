using Exa.Grids.Blueprints;
using Exa.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Exa.Utils;
using UnityEngine;

namespace Exa.IO.Json
{
    public class BlueprintBlocksConverter : JsonConverter<BlueprintBlocks>
    {
        public static readonly string[] SEPARATORS = {"(", ",", ")"};

        public override BlueprintBlocks ReadJson(JsonReader reader, Type objectType, BlueprintBlocks existingValue,
            bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) return null;

            var blocks = new BlueprintBlocks();
            foreach (var pair in JObject.Load(reader).Unpack()) {
                var vector = pair.key.Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries);

                var key = new Vector2Int {
                    x = Convert.ToInt32(vector[0]),
                    y = Convert.ToInt32(vector[1])
                };

                var value = pair.value.ToObject<BlueprintBlock>(serializer);

                blocks.Add(new AnchoredBlueprintBlock(key, value));
            }

            return blocks;
        }

        public override void WriteJson(JsonWriter writer, BlueprintBlocks value, JsonSerializer serializer) {
            writer.WriteStartObject();
            foreach (var pair in value) {
                writer.WritePropertyName(pair.GridAnchor.ToShortString());
                serializer.Serialize(writer, pair.BlueprintBlock);
            }

            writer.WriteEndObject();
        }
    }
}