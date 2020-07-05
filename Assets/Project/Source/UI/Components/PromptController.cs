using Exa.Generics;
using Exa.UI.Components;
using Exa.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class PromptController : MonoSingleton<PromptController>
    {
        [SerializeField] private Transform ownerObject;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private FormGenerator formGenerator;
        [SerializeField] private Transform yesNoContainer;
        [SerializeField] private Button okButton;
        [SerializeField] private Text promptText;

        public void PromptForm<T>(string message, IUIGroup interactableGroup, ModelDescriptor<T> modelDescriptor, Action<T> onSubmit)
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

        public void PromptYesNo(string message, IUIGroup interactableGroup, Action<bool> onClosePrompt = null)
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

        public void PromptOk(string message, IUIGroup interactableGroup, Action onClosePrompt = null)
        {
            BaseStartPrompt(message, interactableGroup);
            okButton.gameObject.SetActive(true);

            okButton.onClick.AddListener(() =>
            {
                onClosePrompt?.Invoke();
                okButton.gameObject.SetActive(false);
                interactableGroup.Interactable = true;
                gameObject.SetActive(false);
            });
        }

        private void CleanupYesNo(IUIGroup interactableGroup = null)
        {
            yesNoContainer.gameObject.SetActive(false);
            BaseCleanupPrompt(interactableGroup);
        }

        public void BaseStartPrompt(string message, IUIGroup interactableGroup)
        {
            ownerObject.gameObject.SetActive(true);
            interactableGroup.Interactable = false;
            promptText.text = message;
        }

        public void BaseCleanupPrompt(IUIGroup interactableGroup)
        {
            ownerObject.gameObject.SetActive(false);
            interactableGroup.Interactable = true;
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        }
    }
}