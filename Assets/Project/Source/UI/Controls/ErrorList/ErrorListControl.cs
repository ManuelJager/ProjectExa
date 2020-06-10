using Exa.Schemas;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Controls
{
    public class ErrorListControl<TValidator, TValidatorArgs> : MonoBehaviour
        where TValidator : IValidator<TValidatorArgs>, new()
    {
        protected ValidationSchema schema;
        protected TValidator validator;
        protected Dictionary<string, ValidationErrorPanel> panelByError = new Dictionary<string, ValidationErrorPanel>();

        [SerializeField] private GameObject errorPanelPrefab;

        private void Awake()
        {
            var builder = CreateSchemaBuilder();
            schema = builder.Build();
            validator = new TValidator();
        }

        public ValidationResult Validate(TValidatorArgs args)
        {
            return schema.Control(validator, args);
        }

        public ValidationResult Validate(IValidator<TValidatorArgs> validator, TValidatorArgs args)
        {
            return schema.Control(validator, args);
        }

        public void OnEnable()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            schema.lastControlErrors = null;
        }

        // Default schema builder
        public virtual ValidationSchemaBuilder CreateSchemaBuilder()
        {
            return new ValidationSchemaBuilder()
                .OnUnhandledError((errorInstance) =>
                {
                    var id = errorInstance.ErrorId;
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
                    var id = errorInstance.ErrorId;
                    panelByError[id].gameObject.SetActive(false);
                });
        }
    }
}