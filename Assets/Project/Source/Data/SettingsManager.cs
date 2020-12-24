using Exa.UI.Settings;
using UnityEngine;

namespace Exa.Data
{
    public class SettingsManager : MonoBehaviour
    {
        public VideoSettingsPanel videoSettings;
        public AudioSettingsPanel audioSettings;

        public void Load() {
            videoSettings.Init();
            audioSettings.Init();
        }
    }
}