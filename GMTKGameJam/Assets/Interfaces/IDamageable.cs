namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(float damage);
        public void TakeHeal(float health);
        public void Kill();
    }
}
