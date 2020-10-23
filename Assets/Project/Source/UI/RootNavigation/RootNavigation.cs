using Exa.Data;
using Exa.UI.Components;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI
{
    public class RootNavigation : MonoBehaviour
    {
        public BlueprintSelector blueprintSelector;
        public SettingsManager settings;
        public MissionSetup missionSetup;
        public Navigateable navigateable;

        [SerializeField] private RootNavigationContent content;
    }
}