using System.Collections;
using UnityEngine;

namespace Exa.Audio.Music
{
    /// <summary>
    /// Sets the current atmosphere when enabled
    /// </summary>
    public class AtmosphereTrigger : MonoBehaviour
    {
        public Atmosphere targetAtmosphere = Atmosphere.None;

        private void OnEnable()
        {
            StartCoroutine(DelayedSetAtmosphere());
        }

        private IEnumerator DelayedSetAtmosphere()
        {
            yield return 0;

            Systems.Audio.stAudioTrack.SetAtmosphere(targetAtmosphere);
        }
    }
}