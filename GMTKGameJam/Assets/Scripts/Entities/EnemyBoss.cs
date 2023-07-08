using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class EnemyBoss : AdaptiveFighterClass
{
    //
    protected override void MovementLogic()
    {
        throw new System.NotImplementedException();
    }
    //
    protected override void DamageLogic()
    {
        throw new System.NotImplementedException();
    }

    protected override void PickUpWeapon()
    {
        AvailableWeapons[0].OnPickup(Owner.BOSS, 2, 0.5f);
        throw new System.NotImplementedException();
    }

    public void SetData()
    {
        base.SetData();
    }
}
