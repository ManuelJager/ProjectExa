using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    /// <summary>
    /// Provides a generic base class for storing and setting the base values of blocks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BlockTemplate<T> : BlockTemplate
        where T : Block
    {
        [Header("Template partials")] 
        [SerializeField] protected PhysicalTemplatePartial physicalTemplatePartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return new TemplatePartialBase[] {
                physicalTemplatePartial
            };
        }
    }

    public abstract class BlockTemplate : ScriptableObject, IGridTotalsModifier
    {
        [Header("Settings")] 
        public string id;
        public string displayId;
        public BlockCategory category;
        public Sprite thumbnail;
        public Vector2Int size;
        public float strength;
        public GameObject inertPrefab;
        public GameObject alivePrefab;

        private void OnEnable() {
            if (!inertPrefab) {
                Debug.LogWarning("inertPrefab must have a prefab reference");
            }
        }

        public void AddGridTotals(GridTotals totals) {
            totals.Strength += strength;
            
            foreach (var partial in GetTemplatePartials()) {
                partial.AddGridTotals(totals);
            }
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.Strength += strength;
            
            foreach (var partial in GetTemplatePartials()) {
                partial.RemoveGridTotals(totals);
            }
        }

        public T GetValues<T>(BlockContext blockContext)
            where T : struct, IBlockComponentValues {
            return Systems.Blocks.Values.GetValues<T>(blockContext, this);
        }

        public abstract IEnumerable<TemplatePartialBase> GetTemplatePartials();
    }
}