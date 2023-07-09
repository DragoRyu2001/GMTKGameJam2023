using UnityEngine;

public class Assault : Weapon
{
    [SerializeField] private float degreeVariance;
    private Bullet currentBullet;
    public override void StartFiring()
    {
        if (Durability == 0) return;
        base.StartFiring();
    }

    private void TakeShot()
    {
        currentBullet = Instantiate(BulletStats.BulletPrefab, Muzzle.transform.position, transform.rotation);
        float zVal = transform.rotation.eulerAngles.z;
        zVal += Random.Range(-degreeVariance, degreeVariance);
        currentBullet.transform.rotation = Quaternion.Euler(0, 0, zVal);
        currentBullet.SetBullet(this, BulletStats, Owner);
        Decay();
    }

    public override void StopFiring()
    {
        Firing = false;
    }
    private void Update()
    {
        if (Firing)
        {
            if (TimeBetweenShots <= 0)
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
