using UnityEngine;
using DragoRyu.Utilities;

public class Sniper : Weapon
{
    private Bullet currentBullet;
    public override void StartFiring()
    {
        if (durability == 0) return;
        print("Start");
        base.StartFiring();
    }

    private void TakeShot()
    {
        currentBullet = Instantiate(bulletStats.BulletPrefab, muzzle.transform.position, transform.rotation);
        currentBullet.SetBullet(this, bulletStats, owner);
        Decay();
    }

    public override void StopFiring()
    {
        firing = false;
        print("Stop");
    }
    private void Update()
    {
        if(firing)
        {
            if(timeBetweenShots<=0)
            {
                timeBetweenShots = stats.FireRate;
                TakeShot();
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }    
    }

}
