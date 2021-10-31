using System;
using UnityEngine;

namespace Exa.Gameplay {
    public abstract class SupportDroneTask {
        protected SupportDroneTask(SupportDroneTaskType type) {
            Type = type;
        }

        public SupportDroneTaskType Type { get; private set; }

        public abstract Vector2 GetPosition();
    }
}