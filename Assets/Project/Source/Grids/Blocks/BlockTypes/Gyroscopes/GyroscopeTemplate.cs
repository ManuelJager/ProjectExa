using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Gyroscope")]
    public class GyroscopeTemplate : BlockTemplate<Gyroscope>, IGyroscopeTemplatePartial
    {
        [SerializeField] private GyroscopeTemplatePartial gyroscopeTemplatePartial;

        public GyroscopeTemplatePartial GyroscopeTemplatePartial 
        { 
            get => gyroscopeTemplatePartial; 
            set => gyroscopeTemplatePartial = value; 
        }

        protected override Gyroscope BuildOnGameObject(GameObject gameObject)
        {
            var instance = base.BuildOnGameObject(gameObject);
            instance.GyroscopeBehaviour = AddBlockBehaviour<GyroscopeBehaviour>(instance);
            return instance;
        }

        protected override IEnumerable<TemplatePartialBase> GetTemplatePartials()
        {
            return base.GetTemplatePartials()
                .Append(gyroscopeTemplatePartial);
        }
    }
}