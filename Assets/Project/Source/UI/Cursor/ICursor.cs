using Exa.Generics;

namespace Exa.UI
{
    public enum CursorState
    {
        idle,
        active,
        remove,
        info,
        input
    }

    public interface ICursor
    {
        MarkerContainer HoverMarkerContainer { get; }
        void SetActive(bool active);
        void SetState(CursorState cursorState);
        void OnEnterViewport();
        void OnExitViewport();
    }
}