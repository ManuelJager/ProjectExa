using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.AI
{
    public class ShipAIDebugTooltip : TooltipBase
    {
        [SerializeField] private Text text;
        private ShipAI shipAI;

        public void Show(ShipAI shipAI)
        {
            this.shipAI = shipAI;
            gameObject.SetActive(true);
            Update();
        }

        protected override void Update()
        {
            base.Update();
            text.text = shipAI.ToString();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}