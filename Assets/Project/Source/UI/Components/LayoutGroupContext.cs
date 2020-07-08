using Exa.Utils;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components
{
    /// <summary>
    /// Sets the ignore layout property to true on the given layout element when there are no children on the transform
    /// </summary>
    public class LayoutGroupContext : MonoBehaviour
    {
        [SerializeField] private LayoutElement layoutElement;

        private void OnTransformChildrenChanged()
        {
            UpdateActiveSelf();
        }

        public void UpdateActiveSelf()
        {
            layoutElement.ignoreLayout = transform
                .GetChildren()
                .Where(transform => transform.gameObject.activeSelf)
                .Count() == 0;
        }
    }
}