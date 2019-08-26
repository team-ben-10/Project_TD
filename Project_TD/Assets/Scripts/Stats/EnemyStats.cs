using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] GameObject enemyDeathEffect;
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }


    public override void Die()
    {
        Destroy(gameObject);
        GameManager.instance.EnemyData++;
        Destroy(Instantiate(enemyDeathEffect, transform.position, Quaternion.identity), 3f);
    }

    protected override void Start()
    {
        base.Start();
    }
}
