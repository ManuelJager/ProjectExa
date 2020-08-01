using Exa.Gameplay;
using Exa.UI.Components;
using Exa.Utils;
using UnityEngine;

namespace Exa
{
    public class MissionManager : MonoSingleton<MissionManager>
    {
        public Navigateable navigateable;
        public ShipFactory shipFactory;

        [SerializeField] private GameplayInputController gameplayInputController;

        protected override void Awake()
        {
            base.Awake();
            shipFactory.CreateFriendly("defaultMothership");
        }
    }
}