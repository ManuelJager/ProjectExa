using Exa.Grids.Blueprints;
using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class BlueprintDetails : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private Image image;
        [SerializeField] private Text nameText;
        [SerializeField] private PropertyView blockCountView;

        public void Reflect(Blueprint blueprint)
        {
            if (blueprint == null)
            {
                container.SetActive(false);
                return;
            }
            if (!container.activeSelf) container.SetActive(true);

            nameText.text = blueprint.name;
            blockCountView.Reflect(new Generics.ValueContext 
            {
                name = "Blocks",
                value = blueprint.blocks.Count.ToString()
            });
        }

        private void OnEnable()
        {
            container.SetActive(false);
        }
    }
}