﻿using Exa.Utils;
using Exa.Validation;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Controls
{
    /// <summary>
    /// Used to provide a list of error panels reflecting the current unresolved errors for multiple validators
    /// </summary>
    public class ErrorListController : MonoBehaviour
    {
        [SerializeField] protected RectTransform container;

        // Error container
        protected ValidationState state;

        // Dictionary linking an error with the current view
        protected Dictionary<string, ValidationErrorPanel>
            panelByError = new Dictionary<string, ValidationErrorPanel>();

        [SerializeField] private GameObject errorPanelPrefab;

        private void Awake() {
            var builder = CreateSchemaBuilder();
            state = builder.Build();
        }

        public virtual ValidationResult Validate<T>(IValidator<T> validator, T args) {
            return state.Control(validator, args);
        }

        public void ApplyResults(ValidationResult errors) {
            state.ApplyResults(errors);
        }

        public void OnEnable() {
            container.DestroyChildren();
            panelByError = new Dictionary<string, ValidationErrorPanel>();
            state.lastControlErrors = new Dictionary<IValidator, IEnumerable<ValidationError>>();
        }

        /// <summary>
        /// Supports configuring the
        /// </summary>
        /// <returns></returns>
        public virtual ValidationErrorContainerBuilder CreateSchemaBuilder() {
            return new ValidationErrorContainerBuilder()
                .OnUnhandledError(UnhandledErrorHandler, UnhandledErrorCleaner);
        }

        public virtual void UnhandledErrorHandler(ValidationError validationError) {
            var id = validationError.Id;
            if (!panelByError.ContainsKey(id)) {
                var panel = Instantiate(errorPanelPrefab, container).GetComponent<ValidationErrorPanel>();
                panel.Text = validationError.Message;
                panelByError[id] = panel;
            }
            else {
                var panel = panelByError[id];
                panel.Text = validationError.Message;
                panel.gameObject.SetActive(true);
            }
        }

        public virtual void UnhandledErrorCleaner(ValidationError validationError) {
            var id = validationError.Id;
            panelByError[id].gameObject.SetActive(false);
        }
    }
}