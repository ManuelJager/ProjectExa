using Exa.Gameplay;
using Exa.UI.Components;
using Exa.UI.Gameplay;
using Exa.Utils;
using UnityEngine;

namespace Exa
{
    public class GameSystems : MonoSingleton<GameSystems>
    {
        [SerializeField] private GameplayInputManager gameplayInputManager;
        [SerializeField] private Navigateable navigateable;
        [SerializeField] private ShipFactory shipFactory;
        [SerializeField] private GameplayUI gameplayUI;

        public static GameplayInputManager GameplayInputManager => Instance.gameplayInputManager;
        public static Navigateable Navigateable => Instance.navigateable;
        public static ShipFactory ShipFactory => Instance.shipFactory;
        public static GameplayUI UI => Instance.gameplayUI;

        protected override void Awake()
        {
            base.Awake();
            shipFactory.CreateFriendly("defaultScout", new Vector2(-20, 20));
            shipFactory.CreateFriendly("defaultScout", new Vector2(0, 20));
            shipFactory.CreateFriendly("defaultScout", new Vector2(20, 20));
            shipFactory.CreateFriendly("defaultMothership", new Vector2(0, -20));
        }
    }
}