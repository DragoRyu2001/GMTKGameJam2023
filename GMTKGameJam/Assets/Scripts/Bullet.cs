using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using SODefinitions;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    private BulletSO _bulletSO;
    private Sprite _sprite;
    private bool _canMove;
    private float _damage;
    // ReSharper disable once InconsistentNaming
    public void SetBullet(Weapon weapon, BulletSO bulletSO, Owner owner)
    {
        _bulletSO = bulletSO;
        _sprite = GetBulletSprite(weapon.GetType());
        _damage = bulletSO.BaseDamage;
        _canMove = true;
        SetLayer(owner);
        Destroy(gameObject, 5f);
    }

    private void SetLayer(Owner owner)
    {
        gameObject.layer = owner switch
        {
            Owner.PLAYER => LayerMask.NameToLayer("PlayerBullet"),
            Owner.ENEMY => LayerMask.NameToLayer("EnemyBullet"),
            Owner.BOSS => LayerMask.NameToLayer("BossBullet"),
            _ => gameObject.layer
        };
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        Transform trans = transform;
        trans.position += trans.up * (_bulletSO.BaseSpeed * Time.fixedDeltaTime);
    }
    
    private Sprite GetBulletSprite(System.Type weaponType)
    {
        return _bulletSO.WeaponSpecific[0];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
