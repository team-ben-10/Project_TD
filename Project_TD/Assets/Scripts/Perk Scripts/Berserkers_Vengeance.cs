using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Berserkers Vengeance", menuName = "Perks/New Berserkers Vengeance")]
public class Berserkers_Vengeance : Perk
{
    public override void OnEquip(Weapon weapon, PlayerStats player)
    {
        weapon.damage *= 1 + value / 100;
    }
}
