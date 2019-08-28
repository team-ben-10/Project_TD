using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Strong Bullets", menuName = "Perks/New Strong Bullets")]
public class Strong_Bullets : Perk
{
    public override void OnHit(Weapon weapon, CharacterStats enemy, PlayerStats player)
    {
        var enemyRB = enemy.gameObject.GetComponent<Rigidbody2D>();
        Vector2 dir = (player.transform.position - enemy.transform.position).normalized;
        dir *= -1;
        enemyRB.velocity += dir*value;
    }
}
