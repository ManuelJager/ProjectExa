using DG.Tweening;
using Exa.UI.Controls;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Gameplay
{
    public class MissionState : MonoBehaviour
    {
        [SerializeField] private Text headerText;
        [SerializeField] private Text infoText;
        [SerializeField] private LayoutElement editorButtonLayout;
        [SerializeField] private ButtonControl editorButton;

        private Tween editorButtonLayoutTween;

        private void Awake() {
            editorButton.OnClick.AddListener(() => {
                Debug.Log("Asd");
            });
        }
        
        public void SetText(string headerText, string infoText) {
            this.headerText.text = headerText;
            this.infoText.text = infoText;
        }

        public void ShowEditorButton() {
            editorButton.Button.interactable = true;
            var targetSize = editorButton.GetRectTransform().rect.width;
            editorButtonLayout.DOPreferredWidth(targetSize, 0.5f)
                .Replace(ref editorButtonLayoutTween);
        }

        public void HideEditorButton() {
            editorButton.Button.interactable = false;
            editorButtonLayout.DOPreferredWidth(0f, 0.5f)
                .Replace(ref editorButtonLayoutTween);
        }
    }
}

