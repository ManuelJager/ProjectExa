using Exa.Ships;
using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.AI
{
    public class ShipDebugTooltip : TooltipBase
    {
        [SerializeField] private Text text;
        private Ship ship;

        public void Show(Ship ship)
        {
            this.ship = ship;
            gameObject.SetActive(true);
            Update();
        }

        protected override void Update()
        {
            base.Update();
            text.text = ship.ToString(0);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}