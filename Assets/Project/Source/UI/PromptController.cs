using Exa.Generics;
using Exa.UI.Components;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class PromptController : MonoBehaviour
    {
        [SerializeField] private Transform _ownerObject;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private FormGenerator _formGenerator;
        [SerializeField] private Transform _yesNoContainer;
        [SerializeField] private Button _okButton;
        [SerializeField] private Text _promptText;

        public void PromptForm<T>(string message, IUiGroup uiGroup, ModelDescriptor<T> modelDescriptor, Action<T> onSubmit)
        {
            BaseStartPrompt(message, uiGroup);
            _okButton.gameObject.SetActive(true);
            _formGenerator.gameObject.SetActive(true);
            _formGenerator.GenerateForm(modelDescriptor);

            _okButton.onClick.AddListener(() =>
            {
                onSubmit(modelDescriptor.FromDescriptor());
                _okButton.gameObject.SetActive(false);
                _formGenerator.gameObject.SetActive(false);
                BaseCleanupPrompt(uiGroup);
            });
        }

        public void PromptYesNo(string message, IUiGroup uiGroup, Action<bool> onClosePrompt = null)
        {
            BaseStartPrompt(message, uiGroup);
            _yesNoContainer.gameObject.SetActive(true);

            _yesButton.onClick.AddListener(() =>
            {
                onClosePrompt?.Invoke(true);
                CleanupYesNo(uiGroup);
            });

            _noButton.onClick.AddListener(() =>
            {
                onClosePrompt?.Invoke(false);
                CleanupYesNo(uiGroup);
            });
        }

        public void PromptOk(string message, IUiGroup uiGroup, Action onClosePrompt = null)
        {
            BaseStartPrompt(message, uiGroup);
            _okButton.gameObject.SetActive(true);

            _okButton.onClick.AddListener(() =>
            {
                onClosePrompt?.Invoke();
                _okButton.gameObject.SetActive(false);
                uiGroup.Interactable = true;
                gameObject.SetActive(false);
            });
        }

        private void CleanupYesNo(IUiGroup uiGroup = null)
        {
            _yesNoContainer.gameObject.SetActive(false);
            BaseCleanupPrompt(uiGroup);
        }

        public void BaseStartPrompt(string message, IUiGroup uiGroup)
        {
            _ownerObject.gameObject.SetActive(true);
            uiGroup.Interactable = false;
            _promptText.text = message;
        }

        public void BaseCleanupPrompt(IUiGroup uiGroup)
        {
            _ownerObject.gameObject.SetActive(false);
            uiGroup.Interactable = true;
            _okButton.onClick.RemoveAllListeners();
            _yesButton.onClick.RemoveAllListeners();
            _noButton.onClick.RemoveAllListeners();
        }
    }
}