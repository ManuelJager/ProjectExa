namespace Exa.Pooling
{
    public interface IPool<in T>
        where T : PoolMember
    {
        void Configure(PoolSettings poolSettings);

        PoolMember Retrieve();

        bool Return(T poolMember);
    }
}