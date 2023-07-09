using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Player : AdaptiveFighterClass
    {
        [Header("Dash Variables")]
        [SerializeField] private float dashCooldown;
        [SerializeField] private float nullRadius;
        [SerializeField] private float currentDashCooldown;

        private Camera _camera;
        private MovementScript _movement;


        private Bullet currentBullet;
        private IDamageable currentDamageable;
        //input and Update position
        protected override void MovementLogic()
        {
            Vector2 inputAxis = Vector2.zero;
            inputAxis.x = Input.GetAxisRaw("Horizontal");
            inputAxis.y = Input.GetAxisRaw("Vertical");
            _movement.SetMovement(inputAxis);

            if (Input.GetKeyDown(KeyCode.LeftShift)||Input.GetMouseButtonDown(1))
            {
                if (currentDashCooldown > 0) return;

                Debug.Log("Dashing");

                currentDashCooldown = dashCooldown; 
                _movement.Dash();
                Nullify();
                StartCoroutine(StartCooldown());
            }
        }

        private void Nullify()
        {
            Collider2D[] collArray = Physics2D.OverlapCircleAll(transform.position, nullRadius);
            
            if(collArray == null || collArray.Length == 0) return;

            foreach(Collider2D coll in collArray)
            {
                if(coll.TryGetComponent(out currentBullet))
                {
                    Destroy(currentBullet.gameObject);
                }
                else if(coll.TryGetComponent(out currentDamageable))
                {
                    currentDamageable.TakeDamage(10);
                }
            }
            
        }

        private IEnumerator StartCooldown()
        {
            while(currentDashCooldown>0)
            {
                yield return null;
                currentDashCooldown -= Time.deltaTime;
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