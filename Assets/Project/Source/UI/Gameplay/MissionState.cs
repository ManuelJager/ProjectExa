using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Gameplay
{
    public class MissionState : MonoBehaviour
    {
        [SerializeField] private Text headerText;
        [SerializeField] private Text infoText;

        public void SetText(string headerText, string infoText) {
            this.headerText.text = headerText;
            this.infoText.text = infoText;
        }
    }
}

