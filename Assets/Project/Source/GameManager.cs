using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Grids.Blueprints.BlueprintEditor;
using Exa.UI;
using Exa.Utils;

namespace Exa
{
    public class GameManager : MonoBehaviourInstance<GameManager>
    {
        public BlockFactory blockFactory;
        public BlueprintManager blueprintManager;
        public ShipEditor shipEditor;
        public PromptController promptController;

        public void Start()
        {
            shipEditor.editorOverlay.inventory.Source = blockFactory.availibleBlockTemplates;
            // Load blocks from unity scriptable objects
            blockFactory.gameObject.SetActive(true);
            // Load blueprints from disk
            blueprintManager.gameObject.SetActive(true);
        }
    }
}