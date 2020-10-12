using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Exa.Math
{
    public static partial class MathUtils
    {
        public static EaseFunction CubicBezier(Vector2 a, Vector2 b)
        {
            return (time, duration, overshootOrAmplitude, period) =>
            {
                // linear for now
                var timePerc = time / duration;
                return timePerc;
            };
        }
    }
}
