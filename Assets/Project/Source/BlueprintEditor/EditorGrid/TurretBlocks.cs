using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class  TurretBlocks : Grid<ABpBlock>
    {
        private Dictionary<ABpBlock, TurretCache> cache;
        private EditorGridTurretLayer turretLayer;

        public TurretBlocks(EditorGridTurretLayer turretLayer) {
            this.turretLayer = turretLayer;
            cache = new Dictionary<ABpBlock, TurretCache>();
        }

        public void AddTurret(ABpBlock block, ITurretTemplate template) {
            var overlay = turretLayer.AddStationaryOverlay(block, template);
            var claims = overlay.GetTurretClaims().ToList();
            cache.Add(block, new TurretCache {
                Template = template,
                Overlay = overlay,
                TurretClaimedTiles = claims
            });

            base.Add(block);
        }

        public IEnumerable<Vector2Int> GetTurretClaims() {
            return cache.Values.SelectMany(cache => cache.TurretClaimedTiles).Distinct();
        }

        public override void Remove(ABpBlock gridMember) {
            cache.Remove(gridMember);
            turretLayer.RemoveStationaryOverlay(gridMember);

            base.Remove(gridMember);
        }

        private struct TurretCache
        {
            public ITurretTemplate Template { get; set; }
            public IEnumerable<Vector2Int> TurretClaimedTiles { get; set; }
            public TurretOverlay Overlay { get; set; }
        }
    }
}