using System;
using System.Collections;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public abstract class Mission : ScriptableObject, ILabeledValue<Mission>
    {
        public string missionName;
        public string missionDescription;

        public string Label => missionName;
        public Mission Value => this;
        public PlayerStation Station { get; protected set; }

        public abstract void Init(MissionArgs args);

        public virtual void Update() {
        }
    }
}