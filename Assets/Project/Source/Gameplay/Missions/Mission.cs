using Exa.Research;
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

        protected ResearchBuilder researchBuilder;

        public virtual void Init(MissionArgs args) {
            researchBuilder = new ResearchBuilder(Systems.Research);
            AddResearchModifiers(researchBuilder);
        }

        public virtual void Unload() {
            researchBuilder.Clear();
        }

        protected virtual void AddResearchModifiers(ResearchBuilder builder) { }

        public virtual void Update() { }
    }
}