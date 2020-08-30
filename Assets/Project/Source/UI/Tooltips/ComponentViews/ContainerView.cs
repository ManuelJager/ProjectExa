using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    public class ContainerView : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
        public Transform container;

        public void SetTabs(int tabs)
        {
            verticalLayoutGroup.padding.left += tabs * 8; 
        }
    }
}