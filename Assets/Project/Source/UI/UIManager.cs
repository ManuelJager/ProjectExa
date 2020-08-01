using Exa.Utils;
using UnityEngine;

namespace Exa.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas root;
        [SerializeField] private RectTransform rootTransform;

        public Canvas Root => root;
        public RectTransform RootTransform => rootTransform;
    }
}