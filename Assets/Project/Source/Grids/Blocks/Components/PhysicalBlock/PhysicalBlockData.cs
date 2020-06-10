namespace Exa.Grids.Blocks.Components
{
    public delegate void DestroyDelegate();

    public struct PhysicalBlockData
    {
        public float health;
        public float armor;

        public event DestroyDelegate Destroy;

        public void TakeDamage(float damage)
        {
            var trueDamage = damage - armor;

            if (trueDamage < 0f) return;

            health -= trueDamage;

            if (health < 0)
            {
                Destroy?.Invoke();
            }
        }
    }
}