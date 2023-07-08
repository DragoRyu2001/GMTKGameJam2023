namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
        public void TakeHeal();
        public void Kill();
    }
}
