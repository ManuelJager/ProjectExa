using UnityEngine;
using UnityEngine.UI;

namespace Exa.Validation
{
    public class ValidationErrorPanel : MonoBehaviour
    {
        [SerializeField] private Text text;

        public string Text
        {
            set => text.text = value;
        }
    }
}