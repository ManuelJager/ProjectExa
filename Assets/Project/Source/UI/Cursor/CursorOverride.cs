namespace Exa.UI
{
    public class CursorOverride
    {
        public readonly CursorState cursorState;
        private readonly object _invoker;

        public CursorOverride(CursorState cursorState)
        {
            this.cursorState = cursorState;
        }
    }
}