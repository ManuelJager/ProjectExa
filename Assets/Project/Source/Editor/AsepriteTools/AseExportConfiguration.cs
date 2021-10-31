using System.Collections.Generic;
using System.Linq;
using Exa.Utils;
using Newtonsoft.Json;

namespace Exa.CustomEditors {
    [JsonObject]
    public class AseExportConfiguration {
        private string[] aseLayers;
        [JsonProperty] private Dictionary<string, LayerConfigurationEntry> layers;

        public AseExportConfiguration() {
            layers = new Dictionary<string, LayerConfigurationEntry>();
        }

        public IEnumerable<(string, LayerConfigurationEntry)> GetSortedLayers() {
            return from entry 
                in layers 
                orderby entry.Value.index
                select (entry.Key, entry.Value);
        }

        public void NormalizeWithAseLayers(IEnumerable<string> currentLayers) {
            aseLayers = currentLayers as string[] ?? currentLayers.ToArray();

            foreach (var (layer, layerConfig) in layers.Unpack().ToList()) {
                if (!aseLayers.Contains(layer)) {
                    layers.Remove(layer);
                }

                if (string.IsNullOrEmpty(layerConfig.name)) {
                    layerConfig.name = layer;
                }
            }

            for (var i = 0; i < aseLayers.Length; i++) {
                var currentLayer = aseLayers[i];
                
                if (!layers.ContainsKey(currentLayer)) {
                    layers.Add(
                        currentLayer,
                        new LayerConfigurationEntry {
                            mergeUp = false,
                            exportOnlyFirstFrame = false,
                            name = currentLayer
                        }
                    );
                }

                layers[currentLayer].index = i;
            }
        }

        /// <summary>
        /// Gets a list of layer groups, by grouping ase layers that are configured to merge up.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GroupedLayerOutput> GetLayerGroups() {
            var temp = null as GroupedLayerOutput;

            foreach (var (layer, layerConfig) in GetSortedLayers()) {
                if (!layerConfig.mergeUp && temp != null) {
                    yield return temp;

                    temp = null;
                }

                temp ??= new GroupedLayerOutput {
                    aseLayers = new List<string>(),
                    configuration = layerConfig
                };

                temp.aseLayers.Add(layer);
            }

            if (temp != null) {
                yield return temp;
            }
        }
    }

    public class GroupedLayerOutput {
        public List<string> aseLayers;
        public LayerConfigurationEntry configuration;
    }

    public class LayerConfigurationEntry {
        // Whether or not to merge the aseprite layer with the layer above
        public bool mergeUp;
        public bool exportOnlyFirstFrame;
        public string name;
        public int index;
    }
}