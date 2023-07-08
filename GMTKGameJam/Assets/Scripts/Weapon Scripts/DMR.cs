using UnityEngine;

public class DMR : Weapon
{
    public override void StartFiring()
    {
        print("Start");
    }

    public override void StopFiring()
    {
       print("Stop");
    }
}
