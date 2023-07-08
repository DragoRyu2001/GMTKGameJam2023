namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
        public void TakeHeal(int health);
        public void Kill();
    }
}
