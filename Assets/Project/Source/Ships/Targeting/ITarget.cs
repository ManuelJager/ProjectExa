﻿using UnityEngine;

namespace Exa.Ships.Targetting
{
    public interface ITarget
    {
        /// <summary>
        /// Gets the world position of the target (not the delta)
        /// </summary>
        /// <param name="current">Current position</param>
        Vector2 GetPosition(Vector2 current);
    }
}