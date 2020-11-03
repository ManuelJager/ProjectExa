using System;
using Exa.Generics;
using Exa.Math;
using UnityEngine;

namespace Exa.Gameplay
{
    [Serializable]
    public class CameraTargetSettings
    {
        public BezierCurveSettings bezierCurveSettings = new BezierCurveSettings(
            p0: Vector2.zero, 
            p1: new Vector2(0.5f, 0.5f),
            p2: new Vector2(0.5f, 0.5f),
            p3: new Vector2(1f, 0f));

        public MinMax<float> zoomMinMax = new MinMax<float>(0.1f, 2.5f);
        public float zoomSpeed = 1f;
        public float orthoMultiplier = 30f;
    }
}