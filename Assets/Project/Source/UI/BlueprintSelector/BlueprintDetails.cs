using Exa.Generics;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class BlueprintDetails : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Image _thumbnailImage;
        [SerializeField] private Text _nameText;
        [SerializeField] private PropertyView _blockCountView;
        [SerializeField] private PropertyView _sizeView;
        [SerializeField] private PropertyView _massView;
        [SerializeField] private PropertyView _energyView;

        public void Reflect(Blueprint blueprint)
        {
            if (blueprint == null)
            {
                _container.SetActive(false);
                return;
            }

            if (!_container.activeSelf) _container.SetActive(true);

            try
            {
                var thumbnailRect = new Rect(0, 0, 512, 512);
                var thumbnailPivot = new Vector2(0.5f, 0.5f);
                _thumbnailImage.sprite = Sprite.Create(blueprint.Thumbnail, thumbnailRect, thumbnailPivot);
            }
            catch
            {
                Debug.LogWarning("Error setting blueprint thumbnail");
            }

            _nameText.text = blueprint.name; 
            var size = blueprint.Blocks.Size.Value;

            _blockCountView.SetValue(blueprint.Blocks.GetMemberCount());
            _sizeView.SetValue($"{size.x}x{size.y}");
            _massView.SetValue($"{blueprint.Blocks.Totals.Mass:0} Tonne");
            _energyView.SetValue($"{blueprint.Blocks.Totals.PowerGeneration:0} KW");
        }

        private void OnEnable()
        {
            _container.SetActive(false);
        }
    }
}