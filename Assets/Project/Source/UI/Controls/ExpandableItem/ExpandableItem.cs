using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class ExpandableItem : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image shrinkExpandImage;
        [SerializeField] private Text headerText;
        [SerializeField] private Sprite shrinkSprite;
        [SerializeField] private Sprite expandSprite;

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
            shrinkExpandImage.sprite = contentActive ? shrinkSprite : expandSprite;
        }
    }
}