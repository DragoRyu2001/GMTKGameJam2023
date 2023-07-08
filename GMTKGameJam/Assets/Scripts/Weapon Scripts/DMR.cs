using UnityEngine;

public class DMR : Weapon
{
    public override void StartFiring()
    {
        base.StartFiring();
        if (durability == 0) return;
        print("Start");
    }

    public override void StopFiring()
    {
       print("Stop");
    }
}
