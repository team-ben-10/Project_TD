using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : ScriptableObject
{

    [Multiline]
    public string description;
    public float value;
    public Sprite icon;

    public virtual void OnHit(Weapon weapon,CharacterStats enemy, PlayerStats player)
    {

    }

    public virtual void OnKill(Weapon weapon, CharacterStats enemy, PlayerStats player)
    {

    }

    public virtual void OnEquip(Weapon weapon, PlayerStats player)
    {

    }

    public virtual Perk Copy()
    {
        Perk p = (Perk)System.Activator.CreateInstance(GetType());
        p.name = name;
        p.description = description.Replace("[value]", value.ToString());
        p.value = value;
        p.icon = icon;
        return p;
    }
}
