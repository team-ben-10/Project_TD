using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inner Light", menuName = "Perks/New Inner Light")]
public class Inner_Light : Perk
{
    public override void OnEquip(Weapon weapon, PlayerStats player)
    {
        player.MaxTimeOutSideLight *= 1 + value / 100;
    }
}
