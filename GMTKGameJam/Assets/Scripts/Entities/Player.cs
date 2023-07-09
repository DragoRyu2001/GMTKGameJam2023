using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Player : AdaptiveFighterClass
    {
        [FormerlySerializedAs("dashCooldown")]
        [Header("Dash Variables")]
        [SerializeField] private float DashCooldown;
        [SerializeField] private float NullRadius;
        [SerializeField] private float CurrentDashCooldown;

        private Camera _camera;
        private MovementScript _movement;


        private Bullet _currentBullet;
        private IDamageable _currentDamageable;
        //input and Update position
        protected override void MovementLogic()
        {
            Vector2 inputAxis = Vector2.zero;
            inputAxis.x = Input.GetAxisRaw("Horizontal");
            inputAxis.y = Input.GetAxisRaw("Vertical");
            _movement.SetMovement(inputAxis);

            if (Input.GetKeyDown(KeyCode.LeftShift)||Input.GetMouseButtonDown(1))
            {
                if (CurrentDashCooldown > 0) return;

                Debug.Log("Dashing");

                CurrentDashCooldown = DashCooldown; 
                _movement.Dash();
                Nullify();
                StartCoroutine(StartCooldown());
            }
        }

        private void Nullify()
        {
            Collider2D[] collArray = Physics2D.OverlapCircleAll(transform.position, NullRadius);
            
            if(collArray == null || collArray.Length == 0) return;

            foreach(Collider2D coll in collArray)
            {
                if(coll.TryGetComponent(out _currentBullet))
                {
                    Destroy(_currentBullet.gameObject);
                }
                else if(coll.TryGetComponent(out _currentDamageable))
                {
                    _currentDamageable.TakeDamage(10);
                }
            }
            
        }

        private IEnumerator StartCooldown()
        {
            while(CurrentDashCooldown>0)
            {
                yield return null;
                CurrentDashCooldown -= Time.deltaTime;
            }
        }

        //Pass in Weapon Aim Vector2 and Weapon Shoot Input
        protected override void DamageLogic()
        {
            Vector2 mouseToWorldSpacePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            WeaponAimSystem.AimLogic(mouseToWorldSpacePosition);
            if (Input.GetMouseButtonDown(0))
            {
                WeaponAimSystem.StartFiring();
            }

            if (Input.GetMouseButtonUp(0))
            {
                WeaponAimSystem.StopFiring();
            }
        }

        protected override void PickUpWeapon()
        {
            if (Input.GetKeyDown(KeyCode.F) && AvailableWeapons.Count>0)
            {
                Weapon availableWeapon = AvailableWeapons[0];
                SetWeaponStat(availableWeapon);
                WeaponAimSystem.AddWeapon(availableWeapon);
            }
        }

        private static void SetWeaponStat(Weapon availableWeapon)
        {
            float multiplier = PlayerStats.Instance.GetFireRateMultiplier(availableWeapon);
            float durability = PlayerStats.Instance.GetDurability(availableWeapon);
            availableWeapon.OnPickup(Owner.PLAYER, multiplier, PlayerStats.Instance.GetDurability(availableWeapon));
        }

        public void SetData(Camera cam)
        {
            _camera = cam;
            Health = PlayerStats.Instance.GetPlayerHealth();
            if (TryGetComponent(out MovementScript movementScript))
            {
                _movement = movementScript;
            }
            else
            {
                Debug.LogError("Movement Script Could not Be found");
            }
            base.SetData();
        }
    }
}