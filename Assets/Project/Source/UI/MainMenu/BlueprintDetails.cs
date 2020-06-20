using Exa.Generics;
using Exa.Grids.Blueprints;
using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class BlueprintDetails : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private Image image;
        [SerializeField] private Text nameText;
        [SerializeField] private PropertyView blockCountView;
        [SerializeField] private PropertyView sizeView;
        [SerializeField] private PropertyView massView;
        [SerializeField] private PropertyView peakPowerGenerationView;

        public void Reflect(Blueprint blueprint)
        {
            if (blueprint == null)
            {
                container.SetActive(false);
                return;
            }

            if (!container.activeSelf) container.SetActive(true);

            nameText.text = blueprint.name;

            blockCountView.Reflect(new ValueContext
            {
                name = "Blocks",
                value = blueprint.blocks.Count.ToString()
            });

            var size = blueprint.blocks.Size.Value;
            sizeView.Reflect(new ValueContext
            {
                name = "Size",
                value = $"{size.x}x{size.y}"
            });

            massView.Reflect(new ValueContext
            {
                name = "Mass",
                value = $"{blueprint.blocks.Mass / 1000f:0} Tons"
            });

            peakPowerGenerationView.Reflect(new ValueContext
            {
                name = "Power generation",
                value = $"{blueprint.blocks.PeakPowerGeneration:0} KW"
            });
        }

        private void OnEnable()
        {
            container.SetActive(false);
        }
    }
}