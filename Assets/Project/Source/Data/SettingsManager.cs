using System;
using Exa.Generics;
using Exa.Math;
using Exa.UI.Settings;
using System.Linq;
using UnityEngine;

namespace Exa.Data
{
    public class SettingsManager : MonoBehaviour
    {
        public VideoSettingsPanel videoSettings;
        public AudioSettingsPanel audioSettings;

        private readonly int[] acceptedRefreshRates = { 60, 75, 80, 90, 100, 120, 144, 165, 180, 240 };

        public void Load()
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
            if (!selectedResolutions.Any())
                selectedResolutions = Screen.resolutions;
            

            // Get resolition view models
            var resolutions = selectedResolutions
                .Select(resolution => new LabeledValue<object>($"{resolution.width}x{resolution.height}", resolution) as ILabeledValue<object>)
                .Reverse();

            // Get Refresh rate view models
            var refreshRates = selectedResolutions
                .Select(resolution => resolution.refreshRate)
                .Distinct()
                .OrderByDescending(resolution => resolution)
                .Select(refreshRate => new LabeledValue<object>($"{refreshRate} hz", refreshRate) as ILabeledValue<object>);

            videoSettings.refreshRatesDropdown.CreateTabs(refreshRates);

            // Get first refresh rate
            var firstRefreshRate = (int)videoSettings.refreshRatesDropdown.Value;
            videoSettings.resolutionDropdown.CreateTabs(resolutions);
            videoSettings.resolutionDropdown.FilterByRefreshRate(firstRefreshRate);
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
            where TValues : class, IEquatable<TValues>
        {
            settingsTab.settings = new TSettings();
            settingsTab.settings.Load();
            settingsTab.settings.Apply();
            settingsTab.settings.Save();
            settingsTab.ReflectValues(settingsTab.settings.Values);
        }
    }
}