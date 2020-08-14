using Exa.Generics;
using Exa.UI.Controls;
using UnityEngine;

namespace Exa.UI
{
    public class FormGenerator : MonoBehaviour
    {
        [SerializeField] private Transform controlsContainer;

        public void GenerateForm<T>(ModelDescriptor<T> modelDescriptor)
        {
            foreach (Transform child in controlsContainer)
            {
                Destroy(child.gameObject);
            }

            modelDescriptor.GenerateView(controlsContainer);
        }
    }
}