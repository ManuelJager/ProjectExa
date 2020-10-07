﻿using Exa.UI.Components;
using Exa.UI.Controls;
using Exa.Validation;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.ShipEditor
{
    public class BlueprintErrorListController : ErrorListController
    {
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Text _text;

        public override ValidationResult Validate<T>(IValidator<T> validator, T args)
        {
            var result = base.Validate(validator, args);

            _canvasGroup.interactable = result;
            _canvasGroup.alpha = result ? 1 : 0;
            _layoutElement.ignoreLayout = !result;

            return result;
        }
    }
}