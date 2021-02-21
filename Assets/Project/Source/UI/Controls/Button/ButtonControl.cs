using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls
{
    public class ButtonControl : MonoBehaviour
    {
        public static ButtonControl Create(Transform container, string label) {
            return Systems.UI.Controls.CreateButton(container, label);
        }
        
        [SerializeField] private Button button;
        [SerializeField] private Text text;
        [SerializeField] private LayoutElement layoutElement;
        
        public Button Button => button;
        
        public LayoutElement LayoutElement => layoutElement;

        public UnityEvent OnClick => button.onClick;

        public void Setup(string label) {
            text.text = label;
        }
    }
}