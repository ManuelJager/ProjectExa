using Exa.Audio;
using System.Collections.Generic;

namespace Exa.Audio
{
    public class SoundHandleGroupDictionary
    {
        private Dictionary<string, SoundHandleGroup> dict = new Dictionary<string, SoundHandleGroup>();

        public void Add(SoundHandle handle)
        {
            dict[handle.sound.id].Add(handle);
        }

        public void Remove(SoundHandle handle)
        {
            dict[handle.sound.id].Remove(handle);
        }

        public void RegisterGroup(string id)
        {
            dict[id] = new SoundHandleGroup();
        }

        public IEnumerable<SoundHandleGroup> Handles
        {
            get => dict.Values;
        }
    }
}