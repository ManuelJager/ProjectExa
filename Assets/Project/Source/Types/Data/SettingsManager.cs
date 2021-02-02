using Exa.UI.Settings;
using UnityEngine;

namespace Exa.Data
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private VideoSettingsPanel videoSettings;
        [SerializeField] private AudioSettingsPanel audioSettings;

        public ExaVideoSettings VideoSettings => videoSettings.Container;
        public ExaAudioSettings AudioSettings => audioSettings.Container;

        public void Load() {
            videoSettings.Init();
            audioSettings.Init();
        }
    }
}