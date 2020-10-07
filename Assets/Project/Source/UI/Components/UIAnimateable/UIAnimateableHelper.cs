namespace Exa.UI.Components
{
    public static class UiAnimateableHelper
    {
        public static int GetRotation(this AnimationDirection animationDirection)
        {
            return ((int)animationDirection) - 1;
        }
    }
}