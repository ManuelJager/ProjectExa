using Exa.Utils;
using UnityEngine;

namespace Exa.Pooling
{
    public class PoolMember : MonoBehaviour
    {
        public Pool<PoolMember> pool;

        protected virtual void OnDisable()
        {
            if (MiscUtils.IsQuitting) return;

            pool.Return(this);
        }

        protected virtual void OnDestroy()
        {
            pool.total--;
        }
    }
}