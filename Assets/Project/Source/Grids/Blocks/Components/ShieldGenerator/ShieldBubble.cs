using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public class ShieldBubble : MonoBehaviour, IDamageable {
        [SerializeField] private ShieldGeneratorBehaviour shieldGeneratorBehaviour;

        public void SetRadius(float value) {
            transform.localScale = new Vector3(value * 2, value * 2);
        }
        
        public void Raise() {
            gameObject.SetActive(true);
        }

        public void Lower() {
            gameObject.SetActive(false);
        }

        public ReceivedDamage AbsorbDamage(Damage damage) {
            return shieldGeneratorBehaviour.OnReceiveDamage(damage);
        }

        public Block Block { get; set; }
    }
}