using Exa.Bindings;
using Exa.Grids.Blocks;
using Exa.UI;
using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlockTemplateView : MonoBehaviour, IObserver<BlockTemplate>
    {
        public Button button;

        [SerializeField] private Image image;
        [SerializeField] private Hoverable hoverable;
        private BlockTemplate data;

        private void Awake()
        {
            hoverable.onPointerEnter.AddListener(() =>
            {
                VariableTooltipManager.Instance.blockTemplateTooltip.ShowTooltip(data);
            });
            hoverable.onPointerExit.AddListener(() =>
            {
                VariableTooltipManager.Instance.blockTemplateTooltip.HideTooltip();
            });
        }

        public void OnUpdate(BlockTemplate data)
        {
            this.data = data;
            image.sprite = data.Thumbnail;
        }
    }
}