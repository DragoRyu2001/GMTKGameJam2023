using UnityEngine;

public class Assault : Weapon
{
    [SerializeField] private float degreeVariance;
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
        float zVal = transform.rotation.eulerAngles.z;
        zVal += Random.Range(-degreeVariance, degreeVariance);
        currentBullet.transform.rotation = Quaternion.Euler(0, 0, zVal);
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
        if (firing)
        {
            if (timeBetweenShots <= 0)
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
