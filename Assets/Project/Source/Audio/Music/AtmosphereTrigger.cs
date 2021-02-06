using System.Collections;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Audio.Music
{
    /// <summary>
    /// Sets the current atmosphere when enabled
    /// </summary>
    public class AtmosphereTrigger : MonoOverride<Atmosphere>
    {
        protected override OverrideList<Atmosphere> GetPath() {
            return Systems.Audio.Music.Atmospheres;
        }
    }
}