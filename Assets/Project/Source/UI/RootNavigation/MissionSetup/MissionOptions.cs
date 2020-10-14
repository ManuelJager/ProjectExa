using System.Linq;
using Exa.Gameplay.Missions;
using Exa.Generics;
using Exa.SceneManagement;
using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa.UI
{
    public class MissionOptions : MonoBehaviour
    {
        [SerializeField] private MissionBag missionBag;
        [SerializeField] private DropdownControl missionDropdown;

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
    }
}