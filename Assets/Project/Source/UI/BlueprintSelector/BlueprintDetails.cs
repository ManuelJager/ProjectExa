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

            try
            {
                var thumbnailRect = new Rect(0, 0, 512, 512);
                var thumbnailPivot = new Vector2(0.5f, 0.5f);
                thumbnailImage.sprite = Sprite.Create(blueprint.Thumbnail, thumbnailRect, thumbnailPivot);
            }
            catch
            {
                Debug.LogWarning("Error setting blueprint thumbnail");
            }

            nameText.text = blueprint.name;

            blockCountView.Reflect(new NamedValue<string>
            {
                Name = "Blocks",
                Value = blueprint.Blocks.GetMemberCount().ToString()
            });

            var size = blueprint.Blocks.Size.Value;
            sizeView.Reflect(new NamedValue<string>
            {
                Name = "Size",
                Value = $"{size.x}x{size.y}"
            });

            massView.Reflect(new NamedValue<string>
            {
                Name = "Mass",
                Value = $"{blueprint.Blocks.Totals.Mass / 1000f:0} Tons"
            });

            peakPowerGenerationView.Reflect(new NamedValue<string>
            {
                Name = "Power generation",
                Value = $"{blueprint.Blocks.Totals.PeakPowerGeneration:0} KW"
            });
        }

        private void OnEnable()
        {
            container.SetActive(false);
        }
    }
}