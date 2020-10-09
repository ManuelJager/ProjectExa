using UnityEngine;

namespace Exa.Pooling
{
    public class PoolMember : MonoBehaviour
    {
        public Pool<PoolMember> pool;

        protected virtual void OnDisable()
        {
            if (Systems.IsQuitting) return;

            pool.Return(this);
        }

        protected virtual void OnDestroy()
        {
            if (Systems.IsQuitting) return;

            pool.totalMembers--;
        }
    }
}