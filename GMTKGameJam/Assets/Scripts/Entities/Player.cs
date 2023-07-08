using SODefinitions;
using UnityEngine;

namespace Entities
{
    public class Player : AdaptiveFighterClass
    {
        private Camera _camera;
        //input and Update position
        public override void MovementLogic()
        {
            throw new System.NotImplementedException();
        }
        
        //Pass in Weapon Aim Vector2 and Weapon Shoot Input
        public override void DamageLogic()
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

        public void SetData(Camera cam, CharacterSO so)
        {
            base.SetData(so);
            this._camera = cam;
        }
    }
}