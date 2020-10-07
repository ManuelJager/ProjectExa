using Exa.Generics;
using Exa.UI.Controls;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI
{
    public class FormGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _controlsContainer;

        public void GenerateForm<T>(ModelDescriptor<T> modelDescriptor)
        {
            _controlsContainer.DestroyChildren();

            modelDescriptor.GenerateView(_controlsContainer);
        }
    }
}