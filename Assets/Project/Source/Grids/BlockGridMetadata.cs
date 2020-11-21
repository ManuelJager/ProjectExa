using System.Collections.Generic;
using System.Linq;
using Exa.Bindings;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using Exa.Utils;

namespace Exa.Grids
{
    public class BlockGridMetadata : ICollectionObserver<Block>
    {
        private IEnumerable<Block> blocks;

        public TurretList TurretList;

        public BlockGridMetadata(IEnumerable<Block> blocks) {
            this.blocks = blocks;

            TurretList = new TurretList();
        }

        public IEnumerable<Block> QueryByCategory(BlockCategory category) {
            return blocks.Where(block => block.GetMemberCategory().Is(category));
        }

        public IEnumerable<T> QueryByType<T>()
            where T : class {
            foreach (var block in blocks) {
                if (block is T convertedBlock)
                    yield return convertedBlock;
            }
        }

        public void OnAdd(Block value) {
            if (value is ITurret turret)
                TurretList.Add(turret);
        }

        public void OnRemove(Block value) {
            if (value is ITurret turret)
                TurretList.Remove(turret);
        }
    }
}