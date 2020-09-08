using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public interface IThrustVectors
    {
        void Register(IThruster thruster);
        void Unregister(IThruster thruster);
        void SetGraphics(Vector2 directionScalar);
    }
}