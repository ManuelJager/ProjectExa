using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

namespace Exa.UI.Components
{
    public class ExpandableItem : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image shrinkExpandImage;
        [SerializeField] private Text headerText;
        [SerializeField] private UIFlip arrowFlip;

        [Space]
        [SerializeField] private bool contentActive = false;

        public Transform content;

        public string HeaderText
        {
            set => headerText.text = value;
        }

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
            ReflectActive();
        }

        private void OnClick()
        {
            contentActive = !contentActive;
            ReflectActive();
        }

        private void ReflectActive()
        {
            content.gameObject.SetActive(contentActive);
            arrowFlip.vertical = contentActive;
        }
    }
}