using Exa.Data;
using UnityEngine;

namespace Exa.Audio
{
    /// <summary>
    /// Supports an audioclip with additional properties that gives audio tracks more context
    /// </summary>
    [CreateAssetMenu(menuName = "Audio/Sound")]
    public class Sound : RegisterableScriptableObject
    {
        /// <summary>
        /// Global audio identifier
        /// </summary>
        public string id;

        /// <summary>
        /// Type of the audio object
        /// </summary>
        public AudioType audioType;

        /// <summary>
        /// Clip of the audio object
        /// </summary>
        public AudioClip audioClip;

        /// <summary>
        /// Volume the clip should be played at
        /// </summary>
        public float volume = 1f;

        /// <summary>
        /// Pitch the clip should be played at
        /// </summary>
        public float pitch = 1f;

        /// <summary>
        /// Wether or not it cancels other sounds playing on the current track
        /// </summary>
        public bool allowMultipleOnTrack = true;

        /// <summary>
        /// Wether or not multiple sounds of this type can be played simoultaniously
        /// </summary>
        public bool allowMultipleOfType = true;

        public override void RegisterSelf(MainManager gameManager)
        {
        }
    }
}