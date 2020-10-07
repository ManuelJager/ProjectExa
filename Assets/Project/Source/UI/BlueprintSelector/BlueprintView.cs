using Exa.Bindings;
using Exa.Grids.Blueprints;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class BlueprintView : MonoBehaviour, Bindings.IObserver<Blueprint>
    {
        public Button deleteButton;
        public Button button;
        public Hoverable hoverable;
        [SerializeField] private Image _thumbnailImage;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _classText;

        public void OnUpdate(Blueprint data)
        {
            _nameText.text = data.name;
            _classText.text = data.shipClass;

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