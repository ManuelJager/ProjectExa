using Exa.Generics;
using Exa.UI.Components;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class PromptController : MonoBehaviour
    {
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private FormGenerator formGenerator;
        [SerializeField] private Transform yesNoContainer;
        [SerializeField] private Button okButton;
        [SerializeField] private Text promptText;

        public void PromptForm<T>(string message, IInteractableGroup interactableGroup, ModelDescriptor<T> modelDescriptor, Action<T> onSubmit)
        {
            BaseStartPrompt(message, interactableGroup);
            okButton.gameObject.SetActive(true);
            formGenerator.gameObject.SetActive(true);
            formGenerator.GenerateForm(modelDescriptor);

            okButton.onClick.AddListener(() =>
            {
                onSubmit(modelDescriptor.FromDescriptor());
                okButton.gameObject.SetActive(false);
                formGenerator.gameObject.SetActive(false);
                BaseCleanupPrompt(interactableGroup);
            });
        }

        public void PromptYesNo(string message, IInteractableGroup interactableGroup, Action<bool> onClosePrompt = null)
        {
            BaseStartPrompt(message, interactableGroup);
            yesNoContainer.gameObject.SetActive(true);

            yesButton.onClick.AddListener(() =>
            {
                onClosePrompt?.Invoke(true);
                CleanupYesNo(interactableGroup);
            });

            noButton.onClick.AddListener(() =>
            {
                onClosePrompt?.Invoke(false);
                CleanupYesNo(interactableGroup);
            });
        }

        public void PromptOk(string message, IInteractableGroup interactableGroup, Action onClosePrompt = null)
        {
            BaseStartPrompt(message, interactableGroup);
            okButton.gameObject.SetActive(true);

            okButton.onClick.AddListener(() =>
            {
                onClosePrompt?.Invoke();
                okButton.gameObject.SetActive(false);
                interactableGroup.Interactible = true;
                gameObject.SetActive(false);
            });
        }

        private void CleanupYesNo(IInteractableGroup interactableGroup = null)
        {
            yesNoContainer.gameObject.SetActive(false);
            BaseCleanupPrompt(interactableGroup);
        }

        public void BaseStartPrompt(string message, IInteractableGroup interactableGroup)
        {
            gameObject.SetActive(true);
            interactableGroup.Interactible = false;
            promptText.text = message;
        }

        public void BaseCleanupPrompt(IInteractableGroup interactableGroup)
        {
            gameObject.SetActive(false);
            interactableGroup.Interactible = true;
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        }
    }
}