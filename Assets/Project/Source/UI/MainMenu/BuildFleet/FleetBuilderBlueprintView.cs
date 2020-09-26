using Exa.Bindings;
using Exa.Grids.Blueprints;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class FleetBuilderBlueprintView : MonoBehaviour, IObserver<Blueprint>
    {
        public bool Selected { get; set; } = false;

        [SerializeField] private Image thumbnailImage;
        [SerializeField] private Text title;

        public void OnUpdate(Blueprint data)
        {
            title.text = data.name;

            try
            {
                var thumbnailRect = new Rect(0, 0, 512, 512);
                var thumbnailPivot = new Vector2(0.5f, 0.5f);
                thumbnailImage.sprite = Sprite.Create(data.Thumbnail, thumbnailRect, thumbnailPivot);
            }
            catch
            {
                Debug.LogWarning("Error setting blueprint thumbnail");
            }
        }
    }
}