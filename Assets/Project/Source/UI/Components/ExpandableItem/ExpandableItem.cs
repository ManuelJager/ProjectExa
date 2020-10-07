using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    public class ExpandableItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _shrinkExpandImage;
        [SerializeField] private Text _headerText;
        [SerializeField] private UIFlip _arrowFlip;

        [Space]
        [SerializeField] private bool _contentActive = false;

        public Transform content;

        public string HeaderText
        {
            set => _headerText.text = value;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
            ReflectActive();
        }

        private void OnClick()
        {
            _contentActive = !_contentActive;
            ReflectActive();
        }

        private void ReflectActive()
        {
            content.gameObject.SetActive(_contentActive);
            _arrowFlip.vertical = _contentActive;
        }
    }
}