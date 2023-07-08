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
        AvailableWeapons[0].startDecay = true;
        throw new System.NotImplementedException();
    }
}
