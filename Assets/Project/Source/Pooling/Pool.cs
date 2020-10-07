using Exa.Utils;
using System;
using System.Collections;
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
        public int totalMembers = 0;

        private PoolSettings _poolSettings;
        private Stack<T> _poolMembers = new Stack<T>();

        private void Update()
        {
            // Grow the queue
            if (_poolSettings.growToPreferredSize && _poolMembers.Count < _poolSettings.preferredSize)
            {
                _poolMembers.Push(InstantiatePrefab());
            }
        }

        public virtual void Configure(PoolSettings poolSettings)
        {
            this._poolSettings = poolSettings;

            foreach (var poolMember in _poolMembers)
            {
                Destroy(poolMember.gameObject);
            }

            _poolMembers = new Stack<T>(poolSettings.maxSize);

            for (int i = 0; i < poolSettings.preferredSize; i++)
            {
                _poolMembers.Push(InstantiatePrefab());
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

            return TryPush((T) poolMember);
        }

        protected virtual T TryPop()
        {
            if (_poolMembers.Count == 0)
            {
                return InstantiatePrefab();
            }

            return _poolMembers.Pop();
        }

        protected virtual bool TryPush(T poolMember)
        {
            if (_poolMembers.Count > _poolSettings.maxSize)
            {
                Destroy(poolMember.gameObject);
                return false;
            }

            Action action = () => poolMember.transform.SetParent(transform);
            var enumerator = EnumeratorUtils.DelayOneFrame(action);
            Systems.Instance.StartCoroutine(enumerator);
            _poolMembers.Push(poolMember);
            return true;
        }


        protected virtual T InstantiatePrefab()
        {
            totalMembers++;
            var poolMemberGo = Instantiate(_poolSettings.prefab, transform);
            poolMemberGo.name = $"{_poolSettings.prefab.name} ({totalMembers})";

            var poolMember = poolMemberGo.AddComponent<T>();
            poolMember.pool = this as Pool<PoolMember>;

            return poolMember;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _poolMembers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _poolMembers.GetEnumerator();
        }
    }
}