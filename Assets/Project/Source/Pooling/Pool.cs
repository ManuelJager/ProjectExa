using Exa.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Pooling
{
    public class Pool : MonoBehaviour
    {
        private PoolSettings poolSettings;
        private Stack<PoolMember> poolMembers = new Stack<PoolMember>();
        public int total = 0;

        private void Update()
        {
            // Grow the queue
            if (poolSettings.growToPreferredSize && poolMembers.Count < poolSettings.preferredSize)
            {
                poolMembers.Push(InstantiatePrefab());
            }
        }

        public void Configure(PoolSettings poolSettings)
        {
            this.poolSettings = poolSettings;

            foreach (var poolMember in poolMembers)
            {
                Destroy(poolMember.gameObject);
            }

            poolMembers = new Stack<PoolMember>(poolSettings.maxSize);

            for (int i = 0; i < poolSettings.preferredSize; i++)
            {
                poolMembers.Push(InstantiatePrefab());
            }
        }

        public virtual PoolMember Retrieve()
        {
            return TryPop();
        }

        public virtual bool Return(PoolMember poolMember)
        {
            Action action = () => poolMember.transform.SetParent(transform);
            var enumerator = RoutineUtils.DelayOneFrame(action);
            MainManager.Instance.StartCoroutine(enumerator);
            return TryPush(poolMember);
        }

        protected virtual PoolMember TryPop()
        {
            if (poolMembers.Count == 0)
            {
                return InstantiatePrefab();
            }

            return poolMembers.Pop();
        }

        protected virtual bool TryPush(PoolMember poolMember)
        {
            if (poolMembers.Count > poolSettings.maxSize)
            {
                Destroy(poolMember.gameObject);
                return false;
            }

            poolMembers.Push(poolMember);
            return true;
        }


        private PoolMember InstantiatePrefab()
        {
            total++;
            var poolMemberGO = Instantiate(poolSettings.prefab, transform);
            poolMemberGO.name = $"{poolSettings.prefab.name} ({total})";

            var poolMember = poolMemberGO.AddComponent<PoolMember>();
            poolMember.pool = this;

            return poolMember;
        }
    }
}