using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class KeybindingView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void SetKey(string key)
        {
            _text.text = key;
        }
    }
}