namespace Exa.UI
{
    public class CursorOverride
    {
        public readonly CursorState cursorState;
        private readonly object invoker;

        public CursorOverride(CursorState cursorState)
        {
            this.cursorState = cursorState;
        }

        public CursorOverride(CursorState cursorState, object invoker)
        {
            this.cursorState = cursorState;
            this.invoker = invoker;
        }
    }
}