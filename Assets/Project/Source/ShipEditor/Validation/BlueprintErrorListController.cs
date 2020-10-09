using Exa.UI.Controls;
using Exa.Validation;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    public class BlueprintErrorListController : ErrorListController
    {
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text text;

        public override ValidationResult Validate<T>(IValidator<T> validator, T args)
        {
            var result = base.Validate(validator, args);

            canvasGroup.interactable = result;
            canvasGroup.alpha = result ? 1 : 0;
            layoutElement.ignoreLayout = !result;

            return result;
        }
    }
}