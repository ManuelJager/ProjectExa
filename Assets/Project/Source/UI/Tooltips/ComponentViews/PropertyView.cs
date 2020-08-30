using Exa.Generics;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    public class PropertyView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Text keyText;
        [SerializeField] private Text valueText;

        public void Reflect<T>(LabeledValue<T> labeledValue)
        {
            keyText.text = labeledValue.Label;
            valueText.text = labeledValue.Value.ToString();
        }

        public void SetFont(Font font)
        {
            keyText.font = font;
            valueText.font = font;
        }
    }
}