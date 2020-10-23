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

        public void Load()
        {
            videoSettings.Init();
            audioSettings.Init();
        }
    }
}