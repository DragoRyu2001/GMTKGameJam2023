using UnityEngine;

public class DMR : Weapon
{
    private Bullet currentBullet;
    public override void StartFiring()
    {
        if (durability == 0) return;

        currentBullet = Instantiate(bulletStats.BulletPrefab, muzzle.transform.position, transform.rotation);
        currentBullet.SetBullet(this, bulletStats, owner);

        print("Start");
        base.StartFiring();
    }

    public override void StopFiring()
    {
       print("Stop");
    }
}
