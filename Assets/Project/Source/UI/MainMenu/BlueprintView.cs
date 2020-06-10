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
        [SerializeField] private Text nameText;
        [SerializeField] private Text classText;

        public void OnUpdate(Blueprint data)
        {
            nameText.text = data.name;
            classText.text = data.shipClass;
        }
    }
}