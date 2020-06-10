using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class KeyValuePairView : MonoBehaviour
    {
        [SerializeField] private Text keyText;
        [SerializeField] private Text valueText;

        public void Reflect(string value)
        {
            valueText.text = value;
        }
    }
}