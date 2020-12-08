using UnityEngine;
using Exa.SceneManagement;

namespace Exa.Pooling
{
    public class PoolMember : MonoBehaviour
    {
        [HideInInspector] public IPool<PoolMember> pool;

        protected virtual void OnDisable() {
            if (Systems.IsQuitting) return;
            pool.Return(this);
        }

        protected virtual void OnDestroy() {
            if (Systems.IsQuitting) return;
            pool.OnDestroyMember();
        }
    }
}