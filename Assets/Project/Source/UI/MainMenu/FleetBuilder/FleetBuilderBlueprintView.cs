using Exa.Bindings;
using Exa.Grids.Blueprints;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class FleetBuilderBlueprintView : MonoBehaviour, IObserver<Blueprint>
    {
        public bool Selected { get; set; } = false;

        [SerializeField] private Image _thumbnailImage;
        [SerializeField] private Text _title;

        public void OnUpdate(Blueprint data)
        {
            _title.text = data.name;

            try
            {
                var thumbnailRect = new Rect(0, 0, 512, 512);
                var thumbnailPivot = new Vector2(0.5f, 0.5f);
                _thumbnailImage.sprite = Sprite.Create(data.Thumbnail, thumbnailRect, thumbnailPivot);
            }
            catch
            {
                Debug.LogWarning("Error setting blueprint thumbnail");
            }
        }
    }
}