using UnityEngine;
using UnityEngine.UI;

namespace Exa.Schemas
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