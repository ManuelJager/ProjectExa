using System.Collections.Generic;

namespace Exa.Audio
{
    public class SoundHandleGroupDictionary
    {
        private readonly Dictionary<string, SoundHandleGroup> _dict = new Dictionary<string, SoundHandleGroup>();

        public void Add(SoundHandle handle)
        {
            _dict[handle.sound.id].Add(handle);
        }

        public void Remove(SoundHandle handle)
        {
            _dict[handle.sound.id].Remove(handle);
        }

        public void RegisterGroup(string id)
        {
            _dict[id] = new SoundHandleGroup();
        }

        public IEnumerable<SoundHandleGroup> Handles
        {
            get => _dict.Values;
        }
    }
}