using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class EditorGridItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color hoverColor;

        public void SetColor(bool enabled)
        {
            spriteRenderer.color = enabled ? hoverColor : defaultColor;
        }
    }
}