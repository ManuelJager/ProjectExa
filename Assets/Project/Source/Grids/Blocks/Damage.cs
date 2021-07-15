using Exa.Grids.Blocks.BlockTypes;

namespace Exa.Grids.Blocks {
    public interface IDamageable {
        TakenDamage TakeDamage(Damage damage);
        
        Block Block { get; }
    }
    
    public struct Damage {
        public float value;
        public object source;
    }
   
    public struct TakenDamage {
        public float absorbedDamage;
        public float appliedDamage;
    } 
}