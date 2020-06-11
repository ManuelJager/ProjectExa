using Exa.Generics;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class PropertyView : MonoBehaviour
    {
        [SerializeField] private Transform keyContainer;
        [SerializeField] private Text keyText;
        [SerializeField] private Transform valueContainer;
        [SerializeField] private Text valueText;

        public void Reflect(ValueContext valueContext)
        {
            keyText.text = valueContext.name;
            valueText.text = valueContext.value;
        }

        public void SetKeyOnly(bool value)
        {
            keyContainer.gameObject.SetActive(!value);
            valueText.alignment = value ? TextAnchor.MiddleCenter : TextAnchor.MiddleLeft;
        }
    }
}