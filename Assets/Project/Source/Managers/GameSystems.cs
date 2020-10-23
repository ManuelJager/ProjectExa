using System;
using Exa.AI;
using Exa.Gameplay;
using Exa.Gameplay.Missions;
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

        public Mission Mission { get; private set; }

        public void LoadMission(Mission mission, MissionArgs args) {
            if (Mission != null)
                throw new InvalidOperationException("Cannot load a mission without unloading previous one");

            Mission = mission;
            mission.Init(args);
        }
    }
}