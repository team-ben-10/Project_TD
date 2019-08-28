using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Whirlwind Running", menuName = "Perks/New Whirlwind Running")]
public class Whirlwind_Running : Perk
{
    public override void OnEquip(Weapon weapon, PlayerStats player)
    {
        player.GetComponent<PlayerController>().speed *= 1 + value / 100;
    }
}
