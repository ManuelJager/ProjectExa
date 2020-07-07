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
        [SerializeField] private Image thumbnailImage;
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

            var thumbnailRect = new Rect(0, 0, 512, 512);
            var thumbnailPivot = new Vector2(0.5f, 0.5f);
            thumbnailImage.sprite = Sprite.Create(blueprint.Thumbnail, thumbnailRect, thumbnailPivot);

            nameText.text = blueprint.name;

            blockCountView.Reflect(new NamedWrapper<string>
            {
                Name = "Blocks",
                Value = blueprint.Blocks.anchoredBlueprintBlocks.Count.ToString()
            });

            var size = blueprint.Blocks.Size.Value;
            sizeView.Reflect(new NamedWrapper<string>
            {
                Name = "Size",
                Value = $"{size.x}x{size.y}"
            });

            massView.Reflect(new NamedWrapper<string>
            {
                Name = "Mass",
                Value = $"{blueprint.Mass / 1000f:0} Tons"
            });

            peakPowerGenerationView.Reflect(new NamedWrapper<string>
            {
                Name = "Power generation",
                Value = $"{blueprint.PeakPowerGeneration:0} KW"
            });
        }

        private void OnEnable()
        {
            container.SetActive(false);
        }
    }
}