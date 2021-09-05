namespace Exa.UI.Cursor {
    public enum CursorState {
        idle,
        active,
        remove,
        info,
        input
    }

    public interface ICursor {
        CursorFacades CursorFacades { get; }
        
        void SetActive(bool active);

        void SetState(CursorState cursorState);

        void OnEnterViewport();

        void OnExitViewport();
    }
}