using System;
using System.Collections.Generic;
using DragoRyu.Utilities;
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
        
        protected float Health = 0;
        private bool _canUpdate;
        private float _totalHealth;
        public Action<float> TookDamage;
        public Action<bool> WeaponAvailable;
        public void TakeDamage(float damage)
        {
            Health -= damage;
            TookDamage.SafeInvoke(Health/_totalHealth);
            if (Health <= 0)
                Kill();
        }

        public void TakeHeal(float health)
        {
            Health += health;
        }

        public void Kill()
        {
            GameManager.Instance.PlayerDied(this.GetType());
            Destroy(gameObject);
        }

        private void Update()
        {
            if (!_canUpdate) return;
            MovementLogic();
            DamageLogic();
            PickUpWeapon();
        }

        protected virtual void SetData()
        {
            Health += CharacterSo.BaseHealth;
            _totalHealth = Health;
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
            WeaponAvailable.SafeInvoke(true);
        }

        public void RemoveWeaponAvailable(Weapon weapon)
        {
            AvailableWeapons.Remove(weapon);
            if(AvailableWeapons.Count==0)
                WeaponAvailable.SafeInvoke(false);
        }
        
        protected abstract void MovementLogic();
        protected abstract void DamageLogic();

        protected abstract void PickUpWeapon();
    }
}