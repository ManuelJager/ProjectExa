namespace Exa.Grids.Blocks.Components
{
    public class PhysicalBehaviour : BlockBehaviour<PhysicalData>
    {
        public void TakeDamage(float damage, ref PhysicalData data)
        {
            var trueDamage = damage - data.armor;

            if (trueDamage < 0f) return;

            data.hull -= trueDamage;

            if (base.data.hull < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}