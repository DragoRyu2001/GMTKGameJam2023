using System;
using Interfaces;
using SODefinitions;
using UnityEngine;

namespace Entities
{
    public abstract class AdaptiveFighterClass : MonoBehaviour, IDamageable
    {
        private float _health;

        protected WeaponAim WeaponAimSystem;
        protected MovementScript Movement;
        protected CharacterSO CharacterSo;

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
                Kill();
        }

        public void TakeHeal(float health)
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
            _health = so.BaseHealth;
            CharacterSo = so;
            if (TryGetComponent(out MovementScript movementScript))
            {
                Movement = movementScript;
            }
            else
            {
                Debug.LogError("Movement Script Could not Be found");
            }

            if (TryGetComponent(out WeaponAim aim))
            {
                WeaponAimSystem = aim;
            }
            else
            {
                Debug.LogError("Does not have reference to Weapon Aim");
            }
        }

        protected abstract void MovementLogic();
        protected abstract void DamageLogic();
    }
}