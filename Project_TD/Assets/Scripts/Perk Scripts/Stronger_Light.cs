using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

[CreateAssetMenu(fileName = "Stronger Light", menuName = "Perks/New Stronger Light")]
public class Stronger_Light : Perk
{
    public override void OnEquip(Weapon weapon, PlayerStats player)
    {
        player.GetComponentInChildren<Light2D>().pointLightOuterRadius *= 1 + value / 100;
    }
}

