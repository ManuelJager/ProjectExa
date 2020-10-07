using Exa.Bindings;
using Exa.Grids.Blocks;
using Exa.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.ShipEditor
{
    public class BlockTemplateView : MonoBehaviour, IObserver<BlockTemplate>
    {
        public Button button;

        [SerializeField] private Text _blockSizeText;
        [SerializeField] private Image _image;
        [SerializeField] private Hoverable _hoverable;
        private BlockTemplate _data;

        private void Awake()
        {
            _hoverable.onPointerEnter.AddListener(() =>
            {
                Systems.Ui.tooltips.blockTemplateTooltip.Show(ShipContext.DefaultGroup, _data);
            });
            _hoverable.onPointerExit.AddListener(() =>
            {
                Systems.Ui.tooltips.blockTemplateTooltip.Hide();
            });
        }

        public void OnUpdate(BlockTemplate data)
        {
            this._data = data;
            _image.sprite = data.thumbnail;
            _blockSizeText.text = $"{data.size.x}x{data.size.y}";
        }
    }
}