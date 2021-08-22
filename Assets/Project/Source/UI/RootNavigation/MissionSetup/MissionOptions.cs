using System;
using System.Linq;
using Exa.Gameplay.Missions;
using Exa.UI.Controls;
using Exa.UI.Tooltips;
using Exa.Utils;
using Exa.Validation;
using UnityEngine;

namespace Exa.UI {
    public class MissionOptions : MonoBehaviour {
        [SerializeField] private MissionBag missionBag;
        [SerializeField] private DropdownControl missionDropdown;
        [SerializeField] private InteractableAdapter button;
        [SerializeField] private TextTooltipTrigger tooltipTrigger;
        [SerializeField] private Transform container;
        
        public Mission SelectedMission { get; private set; }
        public Transform MissionStartOptionContainer {
            get => container;
        }

        private void Awake() {
            missionDropdown.CreateTabs(
                missionBag,
                (mission, tab) => {
                    tab.gameObject
                        .AddComponent<TextTooltipTrigger>()
                        .SetText(mission.missionDescription);
                }
            );

            missionDropdown.OnValueChange.AddListener(
                item => {
                    var mission = item as Mission;
                    SelectedMission = mission;
                    RenderMissionStartOptions(mission);
                });

            SelectedMission = missionDropdown.Value as Mission;
        }

        private void OnEnable() {
            RenderMissionStartOptions(SelectedMission);
        }

        private void RenderMissionStartOptions(Mission mission) {
            container.GetChildren().ForEach(child => child.gameObject.Destroy());
            mission.BuildStartOptions(this);
            container.gameObject.SetActive(container.GetChildren().Any());
        }

        public void ReflectFleetBuilderResult(ValidationResult result) {
            button.Interactable = result;
            tooltipTrigger.SetText(result.GetFirstBySeverity()?.Message);
        }
    }
}