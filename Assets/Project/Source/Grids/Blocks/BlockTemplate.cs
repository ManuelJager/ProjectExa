using System.Collections.Generic;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks {
    /// <summary>
    ///     Provides a generic base class for storing and setting the base values of blocks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BlockTemplate<T> : BlockTemplate
        where T : Block {
        [Header("Template partials")]
        [SerializeField] protected PhysicalTemplatePartial physicalTemplatePartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return new TemplatePartialBase[] {
                physicalTemplatePartial
            };
        }

        public void SetContextlessValues(T block) {
            foreach (var partial in GetTemplatePartials()) {
                partial.SetValues(block, partial.GetContextlessValues());
            }
        }
    }

    public abstract class BlockTemplate : ScriptableObject, IGridTotalsModifier {
        [Header("Settings")]
        public string id;
        public string displayId;
        public BlockCategory category;
        public Sprite thumbnail;
        public Vector2Int size;
        public BlockMetadata metadata;
        public GameObject inertPrefab;
        public GameObject alivePrefab;

        private void OnEnable() {
            foreach (var partial in GetTemplatePartials()) {
                partial.Template = this;
            }

            if (!inertPrefab) {
                Debug.LogWarning("inertPrefab must have a prefab reference");
            }
        }

        public void AddGridTotals(GridTotals totals) {
            totals.Metadata += metadata;

            foreach (var partial in GetTemplatePartials()) {
                partial.AddGridTotals(totals);
            }
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.Metadata -= metadata;

            foreach (var partial in GetTemplatePartials()) {
                partial.RemoveGridTotals(totals);
            }
        }

        public T GetValues<T>(BlockContext blockContext)
            where T : struct, IBlockComponentValues {
            return S.Blocks.Values.GetValues<T>(blockContext, this);
        }

        public abstract IEnumerable<TemplatePartialBase> GetTemplatePartials();

        public override string ToString() {
            return $"Block template id: {id}";
        }
    }
}