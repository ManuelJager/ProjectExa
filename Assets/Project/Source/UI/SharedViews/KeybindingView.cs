using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class KeybindingView : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void SetKey(string key)
        {
            text.text = key;
        }
    }
}