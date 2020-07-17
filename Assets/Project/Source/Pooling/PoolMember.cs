using Exa.Utils;
using UnityEngine;

namespace Exa.Pooling
{
    public class PoolMember : MonoBehaviour
    {
        public Pool pool;

        private void OnDisable()
        {
            if (MiscUtils.IsQuitting) return;

            pool.Return(this);
        }

        private void OnDestroy()
        {
            pool.total--;
        }
    }
}