using Exa.AI;
using Exa.Gameplay;
using Exa.Ships;
using Exa.UI.Components;
using Exa.UI.Gameplay;
using Exa.Utils;
using UnityEngine;

namespace Exa
{
    public class GameSystems : MonoSingleton<GameSystems>
    {
        [SerializeField] private GameplayInputManager _gameplayInputManager;
        [SerializeField] private Navigateable _navigateable;
        [SerializeField] private ShipFactory _shipFactory;
        [SerializeField] private GameplayUi _gameplayUi;
        [SerializeField] private AiManager _aIManager;

        public static GameplayInputManager GameplayInputManager => Instance._gameplayInputManager;
        public static Navigateable Navigateable => Instance._navigateable;
        public static ShipFactory ShipFactory => Instance._shipFactory;
        public static GameplayUi Ui => Instance._gameplayUi;
        public static AiManager Ai => Instance._aIManager;

        protected override void Awake()
        {
            base.Awake();
            _shipFactory.CreateFriendly("defaultScout", new Vector2(-20, 20));
        }
    }
}