using System.Collections;
using UnityEngine;

namespace Exa.Audio.Music
{
    /// <summary>
    /// Sets the current atmosphere when enabled
    /// </summary>
    public class AtmosphereTrigger : MonoBehaviour
    {
        [SerializeField] private Atmosphere targetAtmosphere = Atmosphere.None;
        [SerializeField] private bool triggerOnEnable = false;

        private void OnEnable() {
            if (triggerOnEnable)
                StartCoroutine(DelayedSetAtmosphere());
        }

        public void Trigger() {
            Systems.Audio.ST_AudioTrack.SetAtmosphere(targetAtmosphere);
        }

        private IEnumerator DelayedSetAtmosphere() {
            yield return null;

            Trigger();
        }
    }
}