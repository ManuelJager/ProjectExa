﻿using Exa.AI;
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
        [SerializeField] private Raycaster raycaster;
        [SerializeField] private Navigateable navigateable;
        [SerializeField] private ShipFactory shipFactory;
        [SerializeField] private GameplayUI gameplayUI;
        [SerializeField] private AIManager aIManager;
        [SerializeField] private SpawnLayer spawnLayer;
        [SerializeField] private BlockGridManager blockGridManager;
        [SerializeField] private PopupManager popupManager;
        [SerializeField] private MissionManager missionManager;

        public static GameplayInputManager GameplayInputManager => Instance.gameplayInputManager;
        public static Raycaster Raycaster => Instance.raycaster;
        public static Navigateable Navigateable => Instance.navigateable;
        public static ShipFactory ShipFactory => Instance.shipFactory;
        public static GameplayUI UI => Instance.gameplayUI;
        public static AIManager AI => Instance.aIManager;
        public static SpawnLayer SpawnLayer => Instance.spawnLayer;
        public static BlockGridManager BlockGridManager => Instance.blockGridManager;
        public static PopupManager PopupManager => Instance.popupManager;
        public static MissionManager MissionManager => Instance.missionManager;
        public static GameObject GameObject => Instance.gameObject;
        
        public static bool IsQuitting => Systems.IsQuitting || Systems.Scenes.GetSceneIsUnloading("Game");
        public static bool IsPaused => UI.IsPaused || MissionManager.IsEditing;
    }
}