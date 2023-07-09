using UnityEngine;
using DragoRyu.Utilities;

public class Sniper : Weapon
{
    private Bullet currentBullet;
    public override void StartFiring()
    {
        if (Durability == 0) return;
        base.StartFiring();
    }

    private void TakeShot()
    {
        currentBullet = Instantiate(BulletStats.BulletPrefab, Muzzle.transform.position, transform.rotation);
        currentBullet.SetBullet(this, BulletStats, Owner);
        Decay();
    }

    public override void StopFiring()
    {
        Firing = false;
    }
    private void Update()
    {
        if(Firing)
        {
            if(TimeBetweenShots<=0)
            {
                TimeBetweenShots = Stats.FireRate;
                TakeShot();
            }
            else
            {
                TimeBetweenShots -= Time.deltaTime;
            }
        }    
    }

}
