using DragoRyu.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private List<Weapon> weaponList;
    [SerializeField] private float orbitRadius;
    [SerializeField] private float lerpRate;
    private Camera cam;

    private Vector2 mousePosition;
    private float radAngle;
    private float phaseDiff;
    // Start is called before the first frame update
    private void Awake()
    {
        weaponList.Capacity = 4;
    }

    void Start()
    {
        cam = Camera.main;
        if (weaponList.Count > 0)
        {
            phaseDiff = 2f * Mathf.PI / weaponList.Count;
        }
        print(phaseDiff * Mathf.Rad2Deg);
    }

    public void AimLogic(Vector2 aimPosition)
    {
        Vector2 direction = aimPosition - transform.position.XY();
        radAngle = Mathf.Atan2(direction.y, direction.x) - 1.571f; // 90 degrees in radian

        for (int i = 0; i < weaponList.Count; i++)
        {
            Weapon weapon = weaponList[i];
            float weaponPosAngle = radAngle + (i * phaseDiff);
            weapon.transform.position = Vector2.Lerp(weapon.transform.position,
                (new Vector2(Mathf.Cos(weaponPosAngle), Mathf.Sin(weaponPosAngle)) * orbitRadius) + transform.position.XY(),
                lerpRate);

            Vector2 weaponDir = aimPosition - weapon.transform.position.XY();
            float perWeaponAngle = Mathf.Atan2(weaponDir.y, direction.x) * Mathf.Rad2Deg - 90f;
            weapon.transform.rotation = Quaternion.Euler(0, 0, perWeaponAngle);
        }
    }

    public void StartFiring()
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            Weapon w = weaponList[i];
            w.StartFiring();
        }
    }
    public void StopFiring()
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            Weapon w = weaponList[i];
            w.StopFiring();
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weaponList.Count < 4)
        {
            weaponList.Add(weapon);
        }
        else
        {
            RemoveWeapon(weaponList[^1]);
            weaponList.Add(weapon);

        }
        weapon.onDecay += RemoveWeapon;
        phaseDiff = 2f * Mathf.PI / weaponList.Count;
        print(phaseDiff * Mathf.Rad2Deg);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (weaponList.Count > 0)
        {
            weaponList.Remove(weapon);
            if (weaponList.Count > 0)
            {
                phaseDiff = 2f * Mathf.PI / weaponList.Count;
            }
        }
        Destroy(weapon.gameObject);
    }
}
