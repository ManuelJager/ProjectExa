using System;

namespace Exa.Data
{
    [Serializable]
    public class ActivePair<T>
    {
        public T active;
        public T inactive;

        public T GetValue(bool active)
        {
            return active ? this.active : this.inactive;
        }
    }
}