using Exa.Research;
using Exa.Types.Generics;
using Exa.UI;
using UnityEngine;

namespace Exa.Gameplay.Missions {
    public abstract class Mission : ScriptableObject, ILabeledValue<Mission> {
        public string missionName;
        public string missionDescription;

        protected ResearchBuilder researchBuilder;

        public string Label {
            get => missionName;
        }

        public Mission Value {
            get => this;
        }

        public virtual void Init(MissionManager manager) {
            researchBuilder = new ResearchBuilder(S.Research);
            AddResearchModifiers(researchBuilder);
        }

        public virtual void Unload() {
            researchBuilder.Clear();
        }

        public virtual void BuildStartOptions(MissionOptions options) { }

        protected virtual void AddResearchModifiers(ResearchBuilder builder) { }

        public virtual void Update() { }
    }
}