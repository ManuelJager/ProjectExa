using Exa.Utils;
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
        // Error container
        protected ValidationErrorContainer container;

        // Dictionary linking an error with the current view
        protected Dictionary<string, ValidationErrorPanel> panelByError = new Dictionary<string, ValidationErrorPanel>();

        [SerializeField] private GameObject errorPanelPrefab;

        private void Awake()
        {
            var builder = CreateSchemaBuilder();
            container = builder.Build();
        }

        public virtual ValidationResult Validate<T>(IValidator<T> validator, T args)
        {
            return container.Control(validator, args);
        }

        public void ApplyResults(ValidationResult errors)
        {
            container.ApplyResults(errors);
        }

        public void OnEnable()
        {
            transform.DestroyChildren();
            panelByError = new Dictionary<string, ValidationErrorPanel>();
            container.lastControlErrors = new Dictionary<IValidator, IEnumerable<ValidationError>>();
        }

        /// <summary>
        /// Supports configuring the
        /// </summary>
        /// <returns></returns>
        public virtual ValidationErrorContainerBuilder CreateSchemaBuilder()
        {
            return new ValidationErrorContainerBuilder()
                .OnUnhandledError(UnhandledErrorHandler, UnhandledErrorCleaner);
        }

        public virtual void UnhandledErrorHandler(ValidationError validationError)
        {
            var id = validationError.Id;
            if (!panelByError.ContainsKey(id))
            {
                var panel = Instantiate(errorPanelPrefab, transform).GetComponent<ValidationErrorPanel>();
                panel.Text = validationError.Message;
                panelByError[id] = panel;
            }
            else
            {
                var panel = panelByError[id];
                panel.Text = validationError.Message;
                panel.gameObject.SetActive(true);
            }
        }

        public virtual void UnhandledErrorCleaner(ValidationError validationError)
        {
            var id = validationError.Id;
            panelByError[id].gameObject.SetActive(false);
        }
    }
}