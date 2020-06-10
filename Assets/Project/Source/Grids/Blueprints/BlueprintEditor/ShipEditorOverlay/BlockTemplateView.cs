using Exa.Bindings;
using Exa.Grids.Blocks;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class BlockTemplateView : MonoBehaviour, IObserver<BlockTemplate>
    {
        public Button button;

        [SerializeField] private Image image;

        public void OnUpdate(BlockTemplate data)
        {
            image.sprite = data.thumbnail;
        }
    }
}