using System;
using Exa.Generics;
using Exa.Math;
using UnityEngine;

namespace Exa.Gameplay
{
    [Serializable]
    public class CameraTargetSettings
    {
        public BezierCurveSettings bezierCurveSettings = new BezierCurveSettings(0.5f, 0, 0.5f, 1);
        public MinMax<float> zoomMultiplier = new MinMax<float>(0.5f, 2.5f);
        public float zoomSpeed = 1f;
    }
}