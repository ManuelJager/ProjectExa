using Exa.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Pooling
{
    public class Pool : Pool<PoolMember>
    {
    }

    [Serializable]
    public class Pool<T> : MonoBehaviour, IPool<T>
        where T : PoolMember
    {
        private PoolSettings poolSettings;
        private Stack<T> poolMembers = new Stack<T>();
        public int total = 0;

        private void Update()
        {
            // Grow the queue
            if (poolSettings.growToPreferredSize && poolMembers.Count < poolSettings.preferredSize)
            {
                poolMembers.Push(InstantiatePrefab());
            }
        }

        public virtual void Configure(PoolSettings poolSettings)
        {
            this.poolSettings = poolSettings;

            foreach (var poolMember in poolMembers)
            {
                Destroy(poolMember.gameObject);
            }

            poolMembers = new Stack<T>(poolSettings.maxSize);

            for (int i = 0; i < poolSettings.preferredSize; i++)
            {
                poolMembers.Push(InstantiatePrefab());
            }
        }

        public virtual T Retrieve()
        {
            return TryPop();
        }

        public virtual bool Return(PoolMember poolMember)
        {
            if (!(poolMember is T))
            {
                throw new ArgumentException($"Pool member type ({poolMember.GetType()}) does not match type ({typeof(T)})");
            }

            Action action = () => poolMember.transform.SetParent(transform);
            var enumerator = RoutineUtils.DelayOneFrame(action);
            MainManager.Instance.StartCoroutine(enumerator);
            return TryPush((T) poolMember);
        }

        protected virtual T TryPop()
        {
            if (poolMembers.Count == 0)
            {
                return InstantiatePrefab();
            }

            return poolMembers.Pop();
        }

        protected virtual bool TryPush(T poolMember)
        {
            if (poolMembers.Count > poolSettings.maxSize)
            {
                Destroy(poolMember.gameObject);
                return false;
            }

            poolMembers.Push(poolMember);
            return true;
        }


        protected virtual T InstantiatePrefab()
        {
            total++;
            var poolMemberGO = Instantiate(poolSettings.prefab, transform);
            poolMemberGO.name = $"{poolSettings.prefab.name} ({total})";

            var poolMember = poolMemberGO.AddComponent<T>();
            poolMember.pool = this as Pool<PoolMember>;

            return poolMember;
        }
    }
}