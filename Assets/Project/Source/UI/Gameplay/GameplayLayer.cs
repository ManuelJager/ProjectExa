using Exa.Gameplay;
using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI.Gameplay
{
    public class GameplayLayer : MonoBehaviour
    {
        public SelectionOverlay selectionOverlay;
        public SelectionHotbar selectionHotbar;
        public SelectionArea selectionArea;
        public CoreHealthBar coreHealthBar;
        public BlockCostsView currentResources;
        public Navigateable navigateable;
        public MissionState missionState;
    }
}