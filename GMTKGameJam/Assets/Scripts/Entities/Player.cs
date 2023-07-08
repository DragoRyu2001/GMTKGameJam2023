using UnityEngine;

namespace Entities
{
    public class Player : AdaptiveFighterClass
    {
        private Camera _camera;

        private MovementScript _movement;
        //input and Update position
        protected override void MovementLogic()
        {
            Vector2 inputAxis = Vector2.zero;
            inputAxis.x = Input.GetAxisRaw("Horizontal");
            inputAxis.y = Input.GetAxisRaw("Vertical");
            _movement.SetMovement(inputAxis);
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //_movement.Dash();
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
            float multiplier = PlayerStats.Instance.GetDamageMultiplier(availableWeapon);
            float durability = PlayerStats.Instance.GetDurability(availableWeapon);
            availableWeapon.OnPickup(Owner.PLAYER, multiplier, durability);
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