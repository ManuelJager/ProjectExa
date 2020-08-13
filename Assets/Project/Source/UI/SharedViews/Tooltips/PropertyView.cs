using Exa.Generics;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class PropertyView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Text keyText;
        [SerializeField] private Text valueText;

        public void Reflect<T>(NamedValue<T> valueContext)
        {
            keyText.text = valueContext.Name;
            valueText.text = valueContext.Value.ToString();
        }
    }
}