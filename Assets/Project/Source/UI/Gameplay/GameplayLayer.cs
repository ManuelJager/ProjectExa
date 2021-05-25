using Exa.Gameplay;
using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI.Gameplay
{
    public class GameplayLayer : Navigateable
    {
        public SelectionOverlay selectionOverlay;
        public SelectionHotbar selectionHotbar;
        public SelectionArea selectionArea;
        public CoreHealthBar coreHealthBar;
        public BlockCostsView currentResources;
        public MissionState missionState;
    }
}