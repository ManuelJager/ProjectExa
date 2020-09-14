using UnityEngine;

namespace Exa.Debugging
{
    public interface IDebugDragable
    {
        Vector2 GetPosition();
        void SetGlobals(Vector2 position, Vector2 velocity);
    }
}