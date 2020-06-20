namespace Exa.Grids.Blocks.Components
{
    public delegate void DestroyDelegate();

    public struct PhysicalBlockData
    {
        public float hull;
        public float armor;

        public event DestroyDelegate Destroy;

        public void TakeDamage(float damage)
        {
            var trueDamage = damage - armor;

            if (trueDamage < 0f) return;

            hull -= trueDamage;

            if (hull < 0)
            {
                Destroy?.Invoke();
            }
        }
    }
}