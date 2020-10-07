namespace Exa.UI
{
    public enum CursorState
    {
        Idle,
        Active,
        Remove,
        Info,
        Input
    }

    public interface ICursor
    {
        void SetActive(bool active);
        void SetState(CursorState cursorState);
        void OnEnterViewport();
        void OnExitViewport();
    }
}