using Exa.Generics;
using Exa.UI.Settings;
using Exa.Utils;
using System.Linq;
using UnityEngine;

namespace Exa.Data
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private VideoSettingsPanel videoSettings;
        [SerializeField] private AudioSettingsPanel audioSettings;
        private int[] acceptedRefreshRates = { 60, 75, 80, 90, 100, 120, 144, 165, 180, 240 };

        private void Start()
        {
            SetUpVideoSettings();
            Load(videoSettings);
            Load(audioSettings);
        }

        private void SetUpVideoSettings()
        {
            var selectedResolutions = Screen
                .resolutions
                .Where(IsAcceptedRefreshRate)
                .Where(IsAcceptedRatio);

            // If the are no selected resolutions,
            if (selectedResolutions.Count() == 0)
            {
                selectedResolutions = Screen.resolutions;
            }

            // Get resolition view models
            var resolutions = selectedResolutions
                .Select((resolution) => new NamedValue<object>
                {
                    Name = $"{resolution.width}x{resolution.height}",
                    Value = resolution
                })
                .Reverse();

            // Get Refresh rate view models
            var refreshRates = selectedResolutions
                .Select((resolution) => resolution.refreshRate)
                .Distinct()
                .OrderByDescending((resolution) => resolution)
                .Select((refreshRate) => new NamedValue<object>
                {
                    Name = $"{refreshRate} hz",
                    Value = refreshRate
                });

            videoSettings.refreshRatesDropdown.CreateTabs("Refresh rate", refreshRates);

            // Get first refresh rate
            var firstRefreshRate = (int)videoSettings.refreshRatesDropdown.Value.Value;
            videoSettings.resolutionDropdown.CreateTabs("Resolution", resolutions);
            videoSettings.resolutionDropdown.FilterByRefreshRate(firstRefreshRate);
            videoSettings.refreshRatesDropdown.SelectFirstActive();
        }

        private bool IsAcceptedRefreshRate(Resolution resolution)
        {
            return acceptedRefreshRates.Contains(resolution.refreshRate);
        }

        private bool IsAcceptedRatio(Resolution resolution)
        {
            var ratio = MathUtils.GetRatio(new Vector2Int
            {
                x = resolution.width,
                y = resolution.height
            });

            return
                IsRatio(ratio, 21, 9) ||
                IsRatio(ratio, 16, 10) ||
                IsRatio(ratio, 16, 9) ||
                IsRatio(ratio, 5, 4) ||
                IsRatio(ratio, 4, 3);
        }

        private bool IsRatio(Vector2Int ratio, int width, int height)
        {
            return ratio.x == width && ratio.y == height;
        }

        private void Load<TSettings, TValues>(SettingsTab<TSettings, TValues> settingsTab)
            where TSettings : SaveableSettings<TValues>, new()
        {
            settingsTab.current = new TSettings();
            settingsTab.current.Load();
            settingsTab.current.Apply();
            settingsTab.ReflectValues(settingsTab.current.Values);
        }
    }
}