using System;
using Exa.Types.Generics;

namespace Exa.UI.Cursor {
    public class CursorFacades : OverrideList<VirtualCursorFacade> {
        public CursorFacades(VirtualCursorFacade defaultValue, Action<VirtualCursorFacade> onValueChange) 
            : base(defaultValue, onValueChange) { }
    }
}