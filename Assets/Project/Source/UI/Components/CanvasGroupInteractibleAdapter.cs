using UnityEngine;

namespace Exa.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupInteractibleAdapter : MonoBehaviour, IInteractableGroup
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool setAlpha;

        [SerializeField] private bool interactible = true;
        public bool Interactable
        {
            get => interactible;
            set
            {
                interactible = value;

                canvasGroup.interactable = value;
                if (setAlpha) canvasGroup.alpha = value ? 1f : 0.5f;
            }
        }

        private void Awake()
        {
            Interactable = interactible;
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}