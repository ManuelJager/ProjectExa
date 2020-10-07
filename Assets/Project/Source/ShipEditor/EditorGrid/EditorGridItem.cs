using UnityEngine;

namespace Exa.ShipEditor
{
    public class EditorGridItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _hoverColor;

        public void SetColor(bool enabled)
        {
            _spriteRenderer.color = enabled ? _hoverColor : _defaultColor;
        }
    }
}