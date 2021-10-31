using System;
using Exa.Grids;

namespace Exa.Gameplay {
    public interface IRepairable {
        public bool IsRepaired { get; }

        public void Repair(float hull);

        public event Action OnRemoved;
    }
}