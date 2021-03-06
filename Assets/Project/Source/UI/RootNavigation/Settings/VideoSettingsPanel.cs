﻿using System;
using System.Collections;
using System.Linq;
using Exa.UI.Controls;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Settings
{
    public class VideoSettingsPanel : SettingsTab<ExaVideoSettings, VideoSettingsValues>
    {
        public ResolutionDropdown resolutionDropdown;
        public DropdownControl refreshRatesDropdown;
        public RadioControl fullscreenRadio;

        private void Awake() {
            refreshRatesDropdown.OnValueChange.AddListener(obj => {
                resolutionDropdown.FilterByRefreshRate((int) obj);
                resolutionDropdown.SelectFirst();
            });
        }

        public override VideoSettingsValues GetSettingsValues() =>
            new VideoSettingsValues {
                resolution = (Resolution) resolutionDropdown.Value,
                fullscreen = fullscreenRadio.Value
            };

        public override void ReflectValues(VideoSettingsValues values) {
            // TODO: Notify user of invalid configuration
            if (!resolutionDropdown.ContainsItem(values.resolution))
                values.resolution = settings.DefaultValues.resolution;

            resolutionDropdown.SetValue(values.resolution, false);
            refreshRatesDropdown.SetValue(values.resolution.refreshRate, false);
            fullscreenRadio.SetValue(values.fullscreen, false);
        }

        public override void Init() {
            settings.Resolutions = new Resolutions();

            refreshRatesDropdown.CreateTabs(settings.Resolutions
                .GetRefreshRateLabels());

            resolutionDropdown.CreateTabs(settings.Resolutions
                .GetResolutionLabels()
                .Reverse());

            // Get first refresh rate
            var firstRefreshRate = (int) refreshRatesDropdown.Value;
            resolutionDropdown.FilterByRefreshRate(firstRefreshRate);

            base.Init();
        }

        public override void ApplyChanges() {
            const int length = 15;
            const string format = "Do you wish to keep these settings? (Reverting in {0} seconds)";

            var currentValues = settings.Clone();
            var setter = Systems.UI.promptController.PromptTextSetter;
            var uiGroup = Systems.UI.root.interactableAdapter;

            var coroutine = StartCoroutine(UpdatePromptCountdown(length, format, setter, () => {
                Apply(currentValues);
                ReflectValues(currentValues);

                Systems.UI.promptController.CleanupYesNo(uiGroup);
            }));

            Systems.UI.promptController.PromptYesNo(format.Format(length), uiGroup, value => {
                StopCoroutine(coroutine);
                if (value) return;

                Apply(currentValues);
                ReflectValues(currentValues);
            });

            Apply(GetSettingsValues());
        }

        private IEnumerator UpdatePromptCountdown(int length, string format, Action<string> setter, Action onCountdownEnd) {
            for (var i = length; i > 0; i--) {
                setter(format.Format(i));
                yield return new WaitForSeconds(1);
            }

            onCountdownEnd();
        }
    }
}