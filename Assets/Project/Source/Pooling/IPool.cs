using System.Collections;
using System.Collections.Generic;

namespace Exa.Pooling
{
    public interface IPool<out T> : IPool, IEnumerable<T>
        where T : PoolMember
    {
        T Retrieve();
    }

    public interface IPool
    {
        void Configure(PoolSettings poolSettings);
        bool Return(PoolMember poolMember);
    }
}