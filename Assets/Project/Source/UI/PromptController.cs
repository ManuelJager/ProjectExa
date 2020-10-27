using System;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI
{
    public class PromptController : MonoBehaviour
    {
        [SerializeField] private Transform ownerObject;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private FormGenerator formGenerator;
        [SerializeField] private Transform yesNoContainer;
        [SerializeField] private Button okButton;
        [SerializeField] private Text promptText;

        public Action<string> PromptTextSetter => value => promptText.text = value;

        public void PromptForm<T>(string message, IUIGroup uiGroup, ModelDescriptor<T> modelDescriptor,
            Action<T> onSubmit) {
            ActivateMessage(message, uiGroup);
            okButton.gameObject.SetActive(true);
            formGenerator.gameObject.SetActive(true);
            formGenerator.GenerateForm(modelDescriptor);

            okButton.onClick.AddListener(() => {
                onSubmit(modelDescriptor.FromDescriptor());
                okButton.gameObject.SetActive(false);
                formGenerator.gameObject.SetActive(false);
                DeactivateMessage(uiGroup);
            });
        }

        public void PromptYesNo(string message, IUIGroup uiGroup, Action<bool> onClosePrompt = null) {
            ActivateMessage(message, uiGroup);
            yesNoContainer.gameObject.SetActive(true);

            yesButton.onClick.AddListener(() => {
                onClosePrompt?.Invoke(true);
                CleanupYesNo(uiGroup);
            });

            noButton.onClick.AddListener(() => {
                onClosePrompt?.Invoke(false);
                CleanupYesNo(uiGroup);
            });
        }

        public void PromptOk(string message, IUIGroup uiGroup, Action onClosePrompt = null) {
            ActivateMessage(message, uiGroup);
            okButton.gameObject.SetActive(true);

            okButton.onClick.AddListener(() => {
                onClosePrompt?.Invoke();
                okButton.gameObject.SetActive(false);
                uiGroup.Interactable = true;
                gameObject.SetActive(false);
            });
        }

        public void CleanupYesNo(IUIGroup uiGroup = null) {
            yesNoContainer.gameObject.SetActive(false);
            DeactivateMessage(uiGroup);
        }

        public void ActivateMessage(string message, IUIGroup uiGroup) {
            ownerObject.gameObject.SetActive(true);
            uiGroup.Interactable = false;
            promptText.text = message;
        }

        public void DeactivateMessage(IUIGroup uiGroup) {
            ownerObject.gameObject.SetActive(false);
            uiGroup.Interactable = true;
            okButton.onClick.RemoveAllListeners();
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        }
    }
}