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
        [SerializeField] private bool expanded = false;

        public Transform content;

        public bool Expanded {
            get => expanded;
            set {
                expanded = value;
                ReflectExpanded();
            }
        }

        public string HeaderText {
            set => headerText.text = value;
        }

        private void Awake() {
            button.onClick.AddListener(OnClick);
            ReflectExpanded();
        }

        private void OnClick() {
            Expanded = !Expanded;
        }

        private void ReflectExpanded() {
            content.gameObject.SetActive(Expanded);
            arrowFlip.vertical = Expanded;
        }
    }
}