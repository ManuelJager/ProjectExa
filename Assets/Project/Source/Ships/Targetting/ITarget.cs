using UnityEngine;

namespace Exa.Ships.Targetting
{
    public interface ITarget
    {
        Vector2 GetPosition(Vector2 current);
    }
}