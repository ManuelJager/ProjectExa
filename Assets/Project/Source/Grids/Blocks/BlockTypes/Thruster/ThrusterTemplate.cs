using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Thruster")]
    public class ThrusterTemplate : BlockTemplate<Thruster>, IThrusterTemplatePartial
    {
        [SerializeField] private ThrusterTemplatePartial thrusterTemplatePartial;

        public ThrusterTemplatePartial ThrusterTemplatePartial 
        { 
            get => thrusterTemplatePartial; 
            set => thrusterTemplatePartial = value; 
        }

        protected override Thruster BuildOnGameObject(GameObject gameObject)
        {
            var instance = base.BuildOnGameObject(gameObject);
            instance.ThrusterBehaviour = AddBlockBehaviour<ThrusterBehaviour>(instance);
            return instance;
        }

        protected override IEnumerable<TemplatePartialBase> GetTemplatePartials()
        {
            return base.GetTemplatePartials()
                .Append(thrusterTemplatePartial);
        }
    }
}