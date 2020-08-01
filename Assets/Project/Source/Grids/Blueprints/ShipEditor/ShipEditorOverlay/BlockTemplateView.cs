using Exa.Bindings;
using Exa.Grids.Blocks;
using Exa.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Grids.Blueprints.Editor
{
    public class BlockTemplateView : MonoBehaviour, IObserver<BlockTemplate>
    {
        public Button button;

        [SerializeField] private Text blockSizeText;
        [SerializeField] private Image image;
        [SerializeField] private Hoverable hoverable;
        private BlockTemplate data;

        private void Awake()
        {
            hoverable.onPointerEnter.AddListener(() =>
            {
                Systems.MainUI.variableTooltipManager.blockTemplateTooltip.ShowTooltip(data);
            });
            hoverable.onPointerExit.AddListener(() =>
            {
                Systems.MainUI.variableTooltipManager.blockTemplateTooltip.HideTooltip();
            });
        }

        public void OnUpdate(BlockTemplate data)
        {
            this.data = data;
            image.sprite = data.thumbnail;
            blockSizeText.text = $"{data.size.x}x{data.size.y}";
        }
    }
}