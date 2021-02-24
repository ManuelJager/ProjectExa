using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Exa.UI.Gameplay
{
    public class MissionState : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Text headerText;
        [SerializeField] private Text infoText;
        [SerializeField] private ElementTracker headerTracker;
        [SerializeField] private ElementTracker buttonTracker;
        [SerializeField] private ElementTracker infoTracker;
        
        [Header("Input")]
        [SerializeField] private InputAction editorAction;

        private void Awake() {
            editorAction.started += (context) => {
                Debug.Log("Open editor");
            };
        }
        
        public void SetText(string headerText, string infoText, bool animate = true) {
            this.headerText.text = headerText;
            this.infoText.text = infoText;

            this.DelayOneFrame(() => {
                headerTracker.TrackOnce(animate);
                infoTracker.TrackOnce(animate);
            });
        }

        public void ShowEditorButton() {
            editorAction.Enable();
            this.DelayOneFrame(buttonTracker.TrackOnce);
        }

        public void HideEditorButton() {
            editorAction.Disable();
            buttonTracker.Hide();
        }
    }
}

