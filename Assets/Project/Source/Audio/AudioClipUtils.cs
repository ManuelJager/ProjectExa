using System;
using System.IO;
using UnityEngine;
using NAudio.Wave;
using UnityEngine.Networking;
using static System.Int16;

namespace Exa.Audio
{
    public class AudioClipUtils
    {
        public static AudioClip GetWavAudioClip(MemoryStream stream, string name) {
            return null;
        }

        private static float[] Pcm16Bit2Floats(byte[] bytes)
        {
            var max = -(float)MinValue;
            var samples = new float[bytes.Length / 2];

            for (var i = 0; i < samples.Length; i++)
            {
                var int16sample = System.BitConverter.ToInt16(bytes, i * 2);
                samples[i] = int16sample / max;
            }

            return samples;
        }
    }
}