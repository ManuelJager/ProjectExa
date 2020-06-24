using Exa.Validation;
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

    public class ErrorListController : MonoBehaviour
    {
        public ValidChangeEvent onValidChange;

        protected ValidationSchema schema;
        protected Dictionary<string, ValidationErrorPanel> panelByError = new Dictionary<string, ValidationErrorPanel>();

        [SerializeField] private GameObject errorPanelPrefab;

        private void Awake()
        {
            var builder = CreateSchemaBuilder();
            schema = builder.Build();
        }

        public ValidationResult Validate<TArgs>(IValidator<TArgs> validator, TArgs args)
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
                }).OnValidChange((valid) =>
                {
                    onValidChange?.Invoke(valid);
                });
        }
    }
}