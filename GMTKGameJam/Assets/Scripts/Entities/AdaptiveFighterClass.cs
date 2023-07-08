using System.Collections.Generic;
using Interfaces;
using SODefinitions;
using UnityEngine;

namespace Entities
{
    public abstract class AdaptiveFighterClass : MonoBehaviour, IDamageable
    {
        [SerializeField] protected CharacterSO CharacterSo;
        
        protected WeaponAim WeaponAimSystem;
        protected List<Weapon> AvailableWeapons = new();
        
        private float _health;
        private bool _canUpdate;
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
            if (!_canUpdate) return;
            MovementLogic();
            DamageLogic();
            PickUpWeapon();
        }

        public virtual void SetData()
        {
            _health = CharacterSo.BaseHealth;
            if (TryGetComponent(out WeaponAim aim))
            {
                WeaponAimSystem = aim;
            }
            else
            {
                Debug.LogError("Does not have reference to Weapon Aim");
            }

            _canUpdate = true;
        }

        public void AddWeaponAvailable(Weapon weapon)
        {
            AvailableWeapons.Add(weapon);
        }

        public void RemoveWeaponAvailable(Weapon weapon)
        {
            AvailableWeapons.Remove(weapon);
        }
        
        protected abstract void MovementLogic();
        protected abstract void DamageLogic();

        protected abstract void PickUpWeapon();
    }
}