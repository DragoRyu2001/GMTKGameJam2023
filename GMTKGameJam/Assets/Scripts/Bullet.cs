using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using SODefinitions;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    private BulletSO _bulletSO;
    private Sprite _sprite;
    private bool _canMove;
    private int _damage;
    // ReSharper disable once InconsistentNaming
    public void SetBullet(Weapon weapon, BulletSO bulletSO)
    {
        this._bulletSO = bulletSO;
        _sprite = GetBulletSprite(weapon.GetType());
        _damage = (int)bulletSO.BaseDamage;
        _canMove = true;
    }
    
    private void FixedUpdate()
    {
        if (!_canMove) return;
        Transform trans = transform;
        trans.position += trans.forward * (_bulletSO.BaseSpeed * Time.fixedDeltaTime);
    }
    
    private Sprite GetBulletSprite(System.Type weaponType)
    {
        return _bulletSO.WeaponSpecific[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_damage);
        }
    }
}
