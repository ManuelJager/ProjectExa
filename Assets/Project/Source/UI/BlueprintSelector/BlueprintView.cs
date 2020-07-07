using Exa.Bindings;
using Exa.Grids.Blueprints;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class BlueprintView : MonoBehaviour, IObserver<Blueprint>
    {
        public Button deleteButton;
        public Button button;
        public Hoverable hoverable;
        [SerializeField] private Image thumbnailImage;
        [SerializeField] private Text nameText;
        [SerializeField] private Text classText;

        public void OnUpdate(Blueprint data)
        {
            nameText.text = data.name;
            classText.text = data.shipClass;
            var thumbnailRect = new Rect(0, 0, 512, 512);
            var thumbnailPivot = new Vector2(0.5f, 0.5f);
            thumbnailImage.sprite = Sprite.Create(data.Thumbnail, thumbnailRect, thumbnailPivot);
        }
    }
}