using Exa.Grids.Blocks.BlockTypes;

namespace Exa.Grids.Blocks {
    public interface IDamageable {
        ReceivedDamage AbsorbDamage(Damage damage);
        
        Block Block { get; }
    }
    
    public struct Damage {
        public float value;
        public object source;
    }
   
    public struct ReceivedDamage {
        public float absorbedDamage;
        public float appliedDamage;
    } 
}