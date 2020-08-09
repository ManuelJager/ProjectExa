using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Provides a generic base class for storing and setting the base values of blocks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BlockTemplate<T> : BlockTemplate, IPhysicalTemplatePartial
        where T : Block
    {
        [SerializeField] private PhysicalTemplatePartial physicalTemplatePartial;

        public PhysicalTemplatePartial PhysicalTemplatePartial 
        { 
            get => physicalTemplatePartial; 
            set => physicalTemplatePartial = value; 
        }

        public virtual void SetValues(T block)
        {
            block.PhysicalBehaviour.data = physicalTemplatePartial.Convert();
        }

        protected virtual T BuildOnGameObject(GameObject gameObject)
        {
            var instance = gameObject.AddComponent<T>();
            instance.PhysicalBehaviour = AddBlockBehaviour<PhysicalBehaviour>(instance);
            return instance;
        }

        public override Block AddBlockOnGameObject(GameObject gameObject)
        {
             return BuildOnGameObject(gameObject);
        }

        public override void SetValues(Block block)
        {
            SetValues((T)block);
        }

        protected S AddBlockBehaviour<S>(T blockInstance)
            where S : BlockBehaviourBase
        {
            var behaviour = blockInstance.gameObject.AddComponent<S>();
            behaviour.block = blockInstance;
            return behaviour;
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(physicalTemplatePartial.GetTooltipComponents());
        }
    }

    public abstract class BlockTemplate : ScriptableObject, ITooltipPresenter
    {
        public string id;
        public string displayId;
        public string category;
        public string displayCategory;
        public Sprite thumbnail;
        public Vector2Int size;
        public GameObject inertPrefab;
        public GameObject alivePrefab;

        private Tooltip tooltip;
        private IEnumerable<Func<BlockTemplate, IBlueprintTotalsModifier>> modifierGetters;

        public bool GeneratePrefab { get; private set; }

        private void OnEnable()
        {
            tooltip = new Tooltip(TooltipComponentFactory);
            modifierGetters = TypeUtils.GetPropertyGetters<BlockTemplate, IBlueprintTotalsModifier>(GetType()).ToList();
            if (!inertPrefab) throw new Exception("inertPrefab must have a prefab reference");
            GeneratePrefab = !alivePrefab;
        }

        public abstract void SetValues(Block block);

        public abstract Block AddBlockOnGameObject(GameObject gameObject);

        public void DynamicallyAddTotals(Blueprint blueprint)
        {
            foreach (var blueprintModifierGetter in modifierGetters)
            {
                var blueprintModifier = blueprintModifierGetter(this);
                blueprintModifier.AddBlueprintTotals(blueprint);
            }
        }

        public void DynamicallyRemoveTotals(Blueprint blueprint)
        {
            foreach (var blueprintModifierGetter in modifierGetters)
            {
                var blueprintModifier = blueprintModifierGetter(this);
                blueprintModifier.AddBlueprintTotals(blueprint);
            }
        }

        public Tooltip GetTooltip()
        {
            return tooltip;
        }

        protected virtual IEnumerable<ITooltipComponent> TooltipComponentFactory() => new ITooltipComponent[]
        {
            new TooltipTitle(displayId)
        };
    }
}