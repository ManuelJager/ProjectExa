using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Gameplay
{
    public partial class GameplayInputManager
    {
        private IRaycastTarget raycastTarget = null;
        private ShipSelection shipSelection;

        public ShipSelection ShipSelection
        {
            get => shipSelection;
            set
            {
                shipSelection = value;
                GameplayManager.GameplayUI.selectionOverlay.Reflect(value);
            }
        }
    }
}
