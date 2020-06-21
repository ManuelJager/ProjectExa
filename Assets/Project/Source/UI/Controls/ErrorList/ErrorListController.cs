using Exa.Schemas;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.UI.Controls
{
    [Serializable]
    public class ValidChangeEvent : UnityEvent<bool>
    {
    }

    public class ErrorListController<TValidator, TValidatorArgs> : MonoBehaviour
        where TValidator : IValidator<TValidatorArgs>, new()
    {
        public ValidChangeEvent onValidChange;

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
                }).OnValidChange((valid) =>
                {
                    onValidChange?.Invoke(valid);
                });
        }
    }
}