using UnityEngine;

public class SMG : Weapon
{
    public override void StartFiring()
    {
        base.StartFiring();
        if (durability == 0) return;

    }

    public override void StopFiring()
    {
        throw new System.NotImplementedException();
    }
}
