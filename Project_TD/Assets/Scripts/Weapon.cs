using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/New Weapon")]
public class Weapon : ScriptableObject
{

    public GameObject weaponOBJ;
    public GameObject shootParticle;
    public Sprite icon;
    [Multiline]
    public string description;
    public float damage;
    public float fireSpeed;
    public string gunFireSound;

    [Header("Custom Shooting")]
    public string customShootingName;
    public float[] customShootingParams;
    public GameObject[] customShootingObjects;

    [Header("Perks")]
    public List<Perk> perks = new List<Perk>();

    public Weapon Copy()
    {
        Weapon w = new Weapon();
        w.name = name;
        w.weaponOBJ = weaponOBJ;
        w.shootParticle = shootParticle;
        w.damage = damage;
        w.fireSpeed = fireSpeed;
        w.description = description;
        w.gunFireSound = gunFireSound;
        w.customShootingName = customShootingName;
        w.customShootingParams = customShootingParams;
        w.customShootingObjects = customShootingObjects;
        w.icon = icon;
        List<Perk> perks = new List<Perk>();
        foreach (var item in this.perks)
        {
            perks.Add(item.Copy());
        }
        w.perks = perks;
        return w;
    }
}
