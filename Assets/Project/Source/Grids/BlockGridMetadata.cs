using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using Exa.Types.Binding;
using Exa.Utils;

namespace Exa.Grids
{
    public class BlockGridMetadata : ICollectionObserver<Block>
    {
        private ObservableCollection<Block> blocks;

        public TurretList TurretList;
        public List<IThruster> ThrusterList;

        public BlockGridMetadata(ObservableCollection<Block> blocks) {
            this.blocks = blocks;
            blocks.Register(this);

            TurretList = new TurretList();
            ThrusterList = new List<IThruster>();
        }

        public IEnumerable<Block> QueryByCategory(BlockCategory category) {
            return blocks.Where(block => block.GetMemberCategory().HasValue(category));
        }

        public IEnumerable<T> QueryByType<T>()
            where T : class {
            foreach (var block in blocks) {
                if (block is T convertedBlock)
                    yield return convertedBlock;
            }
        }

        public void OnAdd(Block value) {
            if (value is ITurretPlatform turret)
                TurretList.Add(turret);
            if (value is IThruster thruster)
                ThrusterList.Add(thruster);
        }

        public void OnRemove(Block value) {
            if (value is ITurretPlatform turret)
                TurretList.Remove(turret);
            if (value is IThruster thruster)
                ThrusterList.Remove(thruster);
        }
    }
}