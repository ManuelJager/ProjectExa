using Exa.AI;
using Exa.Gameplay;
using Exa.Ships;
using Exa.UI.Components;
using Exa.UI.Gameplay;
using Exa.Utils;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa
{
    public class GameSystems : MonoSingleton<GameSystems>
    {
        [SerializeField] private GameplayInputManager gameplayInputManager;
        [SerializeField] private Navigateable navigateable;
        [SerializeField] private ShipFactory shipFactory;
        [SerializeField] private GameplayUI gameplayUI;
        [SerializeField] private AIManager aIManager;

        public static GameplayInputManager GameplayInputManager => Instance.gameplayInputManager;
        public static Navigateable Navigateable => Instance.navigateable;
        public static ShipFactory ShipFactory => Instance.shipFactory;
        public static GameplayUI UI => Instance.gameplayUI;
        public static AIManager AI => Instance.aIManager;

        protected override void Awake()
        {
            base.Awake();
            shipFactory.CreateFriendly("defaultScout", new Vector2(-20, 20));
        }
    }
}