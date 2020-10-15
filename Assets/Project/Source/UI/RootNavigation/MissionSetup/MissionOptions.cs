using System.Linq;
using Exa.Gameplay.Missions;
using Exa.Generics;
using Exa.SceneManagement;
using Exa.UI.Controls;
using Exa.Validation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Exa.UI
{
    public class MissionOptions : MonoBehaviour
    {
        [SerializeField] private MissionBag missionBag;
        [SerializeField] private DropdownControl missionDropdown;
        [SerializeField] private CanvasGroupInteractableAdapter button;

        public Mission SelectedMission { get; private set; }

        private void Awake()
        {
            missionDropdown.CreateTabs(missionBag.Select(mission => new LabeledValue<object>
            {
                Label = mission.missionName,
                Value = mission
            }));
            missionDropdown.OnValueChange.AddListener((obj) => SelectedMission = obj as Mission);

            SelectedMission = missionDropdown.Value as Mission;
        }

        public void ReflectFleetBuilderResult(ValidationResult result)
        {
            button.Interactable = result;
        }
    }
}