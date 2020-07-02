using Exa.Validation;
using System.Collections.Generic;
using UnityEngine;

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

        public ValidationResult Validate<TArgs>(IValidator<TArgs> validator, TArgs args)
        {
            return container.Control(validator, args);
        }

        public void OnEnable()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            panelByError = new Dictionary<string, ValidationErrorPanel>();
            container.lastControlErrors = new Dictionary<string, IEnumerable<ValidationError>>();
        }

        /// <summary>
        /// Supports configuring the
        /// </summary>
        /// <returns></returns>
        public virtual ValidationErrorContainerBuilder CreateSchemaBuilder()
        {
            return new ValidationErrorContainerBuilder()
                .OnUnhandledError((errorInstance) =>
                {
                    var id = errorInstance.Id;
                    if (!panelByError.ContainsKey(id))
                    {
                        var panel = Instantiate(errorPanelPrefab, transform).GetComponent<ValidationErrorPanel>();
                        panel.Text = errorInstance.Message;
                        panelByError[id] = panel;
                    }
                    else
                    {
                        panelByError[id].gameObject.SetActive(true);
                    }
                }, (errorInstance) =>
                {
                    var id = errorInstance.Id;
                    panelByError[id].gameObject.SetActive(false);
                });
        }
    }
}