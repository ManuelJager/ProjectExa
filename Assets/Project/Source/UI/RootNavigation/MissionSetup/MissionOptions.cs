using System.Linq;
using Exa.Gameplay.Missions;
using Exa.Generics;
using Exa.SceneManagement;
using Exa.UI.Controls;
using Exa.UI.Tooltips;
using Exa.Validation;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Exa.UI
{
    public class MissionOptions : MonoBehaviour
    {
        [SerializeField] private MissionBag missionBag;
        [SerializeField] private DropdownControl missionDropdown;
        [SerializeField] private CanvasGroupInteractableAdapter button;
        [SerializeField] private TextTooltipTrigger tooltipTrigger;

        public Mission SelectedMission { get; private set; }

        private void Awake()
        {
            missionDropdown.CreateTabs(missionBag);
            missionDropdown.OnValueChange.AddListener(item => SelectedMission = item as Mission);

            SelectedMission = missionDropdown.Value as Mission;
        }

        public void ReflectFleetBuilderResult(ValidationResult result)
        {
            button.Interactable = result;
            tooltipTrigger.SetText(result.GetFirstBySeverity()?.Message);
        }
    }
}