using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : ScriptableObject
{

    [Multiline]
    public string description;
    string originalDescription;
    public float value;
    public Sprite icon;

    private void OnEnable()
    {
        originalDescription = description;
    }

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
        if(originalDescription != "" && value != 0)
            p.description = originalDescription.Replace("[value]", value.ToString());
        p.originalDescription = originalDescription;
        p.value = value;
        p.icon = icon;
        return p;
    }
}
