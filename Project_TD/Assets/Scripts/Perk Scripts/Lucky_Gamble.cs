using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lucky Gamble", menuName = "Perks/New Lucky Gamble")]
public class Lucky_Gamble : Perk
{
    public override void OnKill(Weapon weapon, CharacterStats enemy, PlayerStats player)
    {
        if (Random.Range(0, 101) < value)
        {
            GameManager.instance.EnemyData++;
        }
    }
}
