using UnityEngine;

public class Assault : Weapon
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
