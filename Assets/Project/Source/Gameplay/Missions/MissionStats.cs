using Exa.Grids.Blocks;

namespace Exa.Gameplay.Missions
{
    public class MissionStats
    {
        public BlockCosts CollectedResources { get; set; }
        public int DestroyedShips { get; set; }

        public int CollectedResourcesScore => CollectedResources.creditCost;
        public int DestroyedShipsScore => DestroyedShips * 100;
        public int TotalScore => CollectedResourcesScore + DestroyedShipsScore;
    }
}