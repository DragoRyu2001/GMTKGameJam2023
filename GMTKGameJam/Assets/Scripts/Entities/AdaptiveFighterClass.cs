using Interfaces;
using UnityEngine;

namespace Entities
{
    public class AdaptiveFighterClass : MonoBehaviour, IDamageable
    {
        public void TakeDamage(int damage)
        {
        
        }

        public void TakeHeal()
        {
        
        }

        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}
