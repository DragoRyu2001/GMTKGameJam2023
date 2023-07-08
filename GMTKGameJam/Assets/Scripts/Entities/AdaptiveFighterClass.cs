using System;
using Interfaces;
using SODefinitions;
using UnityEngine;

namespace Entities
{
    public abstract class AdaptiveFighterClass : MonoBehaviour, IDamageable
    {
        private int _health;
        
        protected WeaponAim WeaponAimSystem;
        protected MovementScript Movement;
        protected CharacterSO CharacterSo;
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
            if(_health<=0)
                Kill();
        }
        public void TakeHeal(int health)
        {
            _health += health;
        }
        public void Kill()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            MovementLogic();
            DamageLogic();
        }
        
        public virtual void SetData(CharacterSO so)
        {
            _health = (int)so.BaseHealth;
            CharacterSo = so;
            if (TryGetComponent(out WeaponAim aim))
            {
                WeaponAimSystem = aim;
            }
            else
            {
                Debug.LogError("Does not have reference to Weapon Aim");
            }
        }
        public abstract void MovementLogic();
        public abstract void DamageLogic();
    }
}
