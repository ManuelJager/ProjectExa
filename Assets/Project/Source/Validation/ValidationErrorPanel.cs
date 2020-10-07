using UnityEngine;
using UnityEngine.UI;

namespace Exa.Validation
{
    /// <summary>
    /// Validation error view
    /// </summary>
    public class ValidationErrorPanel : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public string Text
        {
            set => _text.text = value;
        }
    }
}