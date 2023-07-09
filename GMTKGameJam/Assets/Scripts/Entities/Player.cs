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
        [SerializeField] private ParticleSystem dashParticle;

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
                dashParticle.transform.parent = null;
                dashParticle.Play();
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
                    _currentDamageable.TakeDamage(5);
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
            dashParticle.transform.parent = transform;
            dashParticle.transform.localPosition = Vector3.zero; 
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
                PlayerPrefsManager.IncreaseCoins(5);
            }
        }

        private static void SetWeaponStat(Weapon availableWeapon)
        {
            float multiplier = PlayerStats.Instance.GetFireRateMultiplier(availableWeapon);
            float durability = PlayerStats.Instance.GetDurability(availableWeapon);
            availableWeapon.OnPickup(Owner.PLAYER, multiplier, PlayerStats.Instance.GetDurability(availableWeapon), 6.0f);
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
            dashParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
            base.SetData();
        }
    }
}