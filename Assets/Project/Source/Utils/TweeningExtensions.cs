using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Exa.Utils
{
    public static class TweeningExtensions
    {
        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotate(this Transform target,
            Vector3 endValue, float duration)
        {
            return ShortcutExtensions.DORotate(target, endValue, duration);
        }

        public static TweenerCore<Vector2, Vector2, VectorOptions> DOSizeDelta(this RectTransform target,
            Vector2 endValue, float duration)
        {
            return DOTweenModuleUI.DOSizeDelta(target, endValue, duration);
        }

        public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPos(this RectTransform target,
            Vector2 endValue, float duration)
        {
            return DOTweenModuleUI.DOAnchorPos(target, endValue, duration);
        }
    }
}
