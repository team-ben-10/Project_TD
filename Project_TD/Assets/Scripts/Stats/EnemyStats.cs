using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class EnemyStats : CharacterStats
{
    [SerializeField] GameObject enemyDeathEffect;
    public override bool TakeDamage(float amount)
    {
        Audio_Manager.instance.Play("Enemy Hit");
        return base.TakeDamage(amount);
    }


    public override void Die()
    {
        //CameraShaker.Instance.ShakeOnce(1f, 1f, .05f, .05f);
        Destroy(gameObject);
        GameManager.instance.EnemyData++;
        var weapon = GameManager.instance.Player.GetComponent<WeaponManager>().currentWeapon;
        weapon.perks.ForEach(x => {
            x.OnKill(weapon, this, GameManager.instance.Player.GetComponent<PlayerStats>());
            });
        Destroy(Instantiate(enemyDeathEffect, transform.position, Quaternion.identity), 3f);
    }

    protected override void Start()
    {
        base.Start();
    }
}
