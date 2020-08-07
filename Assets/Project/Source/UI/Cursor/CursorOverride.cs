using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.UI
{
    public class CursorOverride
    {
        public readonly CursorState cursorState;

        public CursorOverride(CursorState cursorState)
        {
            this.cursorState = cursorState;
        }
    }
}
