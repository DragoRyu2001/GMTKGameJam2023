using SODefinitions;
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
            
        }
        
        //Pass in Weapon Aim Vector2 and Weapon Shoot Input
        protected override void DamageLogic()
        {
            Vector2 mouseToWorldSpacePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            //WeaponAimSystem.
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
            throw new System.NotImplementedException();
        }

        public void SetData(Camera cam)
        {
            base.SetData();
            if (TryGetComponent(out MovementScript movementScript))
            {
                _movement = movementScript;
            }
            else
            {
                Debug.LogError("Movement Script Could not Be found");
            }

            this._camera = cam;
        }
    }
}